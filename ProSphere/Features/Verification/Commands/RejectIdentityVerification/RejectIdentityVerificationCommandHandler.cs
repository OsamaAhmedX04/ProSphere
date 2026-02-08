using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.Extensions;
using ProSphere.ExternalServices.Interfaces.Email;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Verification.Commands.RejectIdentityVerification
{
    public class RejectIdentityVerificationCommandHandler : IRequestHandler<RejectIdentityVerificationCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IValidator<RejectIdentityVerificationRequest> _validator;

        public RejectIdentityVerificationCommandHandler(
            IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager,
            IEmailSenderService emailSenderService, IValidator<RejectIdentityVerificationRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _emailSenderService = emailSenderService;
            _validator = validator;
        }

        public async Task<Result> Handle(RejectIdentityVerificationCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command.request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }

            var isModeratorExist = await _userManager.FindByIdAsync(command.moderatorId) != null;
            if (!isModeratorExist)
                return Result.Failure("Moderator Not Found", StatusCodes.Status404NotFound);

            var identityDocument = await _unitOfWork.IdentityVerifications.GetByIdAsync(command.identityDocumentId);
            if (identityDocument == null)
                return Result.Failure("Document Not Exist", StatusCodes.Status404NotFound);

            identityDocument.status = Status.Rejected;
            identityDocument.ReviewedBy = command.moderatorId;
            identityDocument.ReviewedAt = DateTime.UtcNow;
            identityDocument.RejectionReason = command.request.RejectReason;

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Result.Failure("Error: Another Moderator Took A Verification Action", StatusCodes.Status409Conflict);
            }

            var user = await _userManager.FindByIdAsync(identityDocument.UserId);

            _emailSenderService.SendRejectionVerifiedIdentityMail(user!.Email!, user.FirstName, user.LastName, command.request.RejectReason);

            return Result.Success("Identity Is Rejected Successfully");
        }
    }
}
