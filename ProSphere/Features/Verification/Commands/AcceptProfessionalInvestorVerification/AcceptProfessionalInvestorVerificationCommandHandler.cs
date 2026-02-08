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

namespace ProSphere.Features.Verification.Commands.AcceptProfessionalInvestorVerification
{
    public class AcceptProfessionalInvestorVerificationCommandHandler : IRequestHandler<AcceptProfessionalInvestorVerificationCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSenderService _emailSenderService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AcceptProfessionalInvestorVerificationCommandHandler(IUnitOfWork unitOfWork, IEmailSenderService emailSenderService, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _emailSenderService = emailSenderService;
            _userManager = userManager;
        }

        public async Task<Result> Handle(AcceptProfessionalInvestorVerificationCommand command, CancellationToken cancellationToken)
        {
            var isModeratorExist = await _userManager.FindByIdAsync(command.moderatorId) != null;
            if (!isModeratorExist)
                return Result.Failure("Moderator Not Found", StatusCodes.Status404NotFound);

            var professionalDocument = await _unitOfWork.FinancialVerifications.GetByIdAsync(command.professionalDocumentId);
            if (professionalDocument == null || professionalDocument.status == Status.Rejected || professionalDocument.status == Status.Approved)
                return Result.Failure("Document Not Exist", StatusCodes.Status404NotFound);

            professionalDocument.status = Status.Approved;
            professionalDocument.ReviewedAt = DateTime.UtcNow;
            professionalDocument.ReviewedBy = command.moderatorId;

            var investor = await _unitOfWork.Investors.FirstOrDefaultAsync(i => i.Id == professionalDocument.InvestorId);
            if (investor is not null) investor.InvestorLevel = InvestorLevel.Professional;

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Result.Failure("Error: Another Moderator Took A Verification Action", StatusCodes.Status409Conflict);
            }

            BackgroundJob.Enqueue<IDeleteDocumentVerificationJob>(
                service => service.DeleteProfessionalVerificationDocuments(professionalDocument.InvestorId, Status.Rejected)
                );

            var userData = await _userManager.FindByIdAsync(professionalDocument.InvestorId);

            _emailSenderService.SendAcceptanceVerifiedProfessionalMail(userData!.Email!, userData.FirstName, userData.LastName);

            return Result.Success("Professional Is Verified Successfully");
        }
    }
}
