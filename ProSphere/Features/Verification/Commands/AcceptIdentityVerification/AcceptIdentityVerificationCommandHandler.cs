using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.ExternalServices.Interfaces.Email;
using ProSphere.Jobs.Documents.DeleteDocumentVerification;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Verification.Commands.AcceptIdentityVerification
{
    public class AcceptIdentityVerificationCommandHandler : IRequestHandler<AcceptIdentityVerificationCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSenderService _emailSenderService;

        public AcceptIdentityVerificationCommandHandler(
            IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager,
            IEmailSenderService emailSenderService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _emailSenderService = emailSenderService;
        }

        public async Task<Result> Handle(AcceptIdentityVerificationCommand command, CancellationToken cancellationToken)
        {
            var isModeratorExist = await _userManager.FindByIdAsync(command.moderatorId) != null;
            if (!isModeratorExist)
                return Result.Failure("Moderator Not Found", StatusCodes.Status404NotFound);

            var identityDocument = await _unitOfWork.IdentityVerifications.GetByIdAsync(command.identityDocumentId);
            if (identityDocument == null || identityDocument.status == Status.Rejected || identityDocument.status == Status.Approved)
                return Result.Failure("Document Not Exist", StatusCodes.Status404NotFound);

            identityDocument.status = Status.Approved;
            identityDocument.ReviewedBy = command.moderatorId;
            identityDocument.ReviewedAt = DateTime.UtcNow;

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Result.Failure("Error: Another Moderator Took A Verification Action", StatusCodes.Status409Conflict);
            }

            var user = await _userManager.FindByIdAsync(identityDocument.UserId);
            user!.IsVerified = true;
            await _userManager.UpdateAsync(user);

            BackgroundJob.Enqueue<IDeleteDocumentVerificationJob>(
                service => service.DeleteIdentityVerificationDocuments(identityDocument.UserId, Status.Rejected)
                );

            _emailSenderService.SendAcceptanceVerifiedIdentityMail(user.Email!, user.FirstName, user.LastName);

            return Result.Success("Identity Is Verified Successfully");
        }
    }
}
