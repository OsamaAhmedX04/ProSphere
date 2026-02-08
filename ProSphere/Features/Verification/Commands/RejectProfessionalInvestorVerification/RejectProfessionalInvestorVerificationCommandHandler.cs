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

namespace ProSphere.Features.Verification.Commands.RejectProfessionalInvestorVerification
{
    public class RejectProfessionalInvestorVerificationCommandHandler : IRequestHandler<RejectProfessionalInvestorVerificationCommand, Result>
    {
        private readonly IValidator<RejectProfessionalInvestorVerificationRequest> _validator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSenderService _emailSenderService;

        public RejectProfessionalInvestorVerificationCommandHandler(
            IValidator<RejectProfessionalInvestorVerificationRequest> validator, IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager, IEmailSenderService emailSenderService)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _emailSenderService = emailSenderService;
        }

        public async Task<Result> Handle(RejectProfessionalInvestorVerificationCommand command, CancellationToken cancellationToken)
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

            var professionalDocument = await _unitOfWork.ProfessionalVerifications.GetByIdAsync(command.professionalDocumentId);
            if (professionalDocument == null)
                return Result.Failure("Document Not Exist", StatusCodes.Status404NotFound);

            professionalDocument.status = Status.Rejected;
            professionalDocument.ReviewedBy = command.moderatorId;
            professionalDocument.ReviewedAt = DateTime.UtcNow;
            professionalDocument.RejectionReason = command.request.RejectReason;

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Result.Failure("Error: Another Moderator Took A Verification Action", StatusCodes.Status409Conflict);
            }

            var user = await _userManager.FindByIdAsync(professionalDocument.InvestorId);

            _emailSenderService.SendRejectionVerifiedProfessionalMail(user!.Email!, user.FirstName, user.LastName, command.request.RejectReason);

            return Result.Success("Professional Is Rejected Successfully");
        }
    }
}
