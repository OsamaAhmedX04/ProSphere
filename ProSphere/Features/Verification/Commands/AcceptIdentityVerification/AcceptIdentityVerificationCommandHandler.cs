using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProSphere.Domain.Constants.CacheConstants;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.ExternalServices.Interfaces.Email;
using ProSphere.Jobs.Documents.DeleteDocumentVerification;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProSphere.Features.Verification.Commands.AcceptIdentityVerification
{
    public class AcceptIdentityVerificationCommandHandler : IRequestHandler<AcceptIdentityVerificationCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IMemoryCache _cache;

        public AcceptIdentityVerificationCommandHandler(
            IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager,
            IEmailSenderService emailSenderService, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _emailSenderService = emailSenderService;
            _cache = cache;
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

            _cache.Remove(CacheKey.GetInvestorAccountKey(user.UserName!));
            _cache.Remove(CacheKey.GetCreatorAccountKey(user.UserName!));

            return Result.Success("Identity Is Verified Successfully");
        }
    }
}
