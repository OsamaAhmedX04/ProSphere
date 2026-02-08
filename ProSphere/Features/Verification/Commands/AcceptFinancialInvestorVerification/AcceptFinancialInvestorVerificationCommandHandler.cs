using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.ExternalServices.Interfaces.Email;
using ProSphere.ExternalServices.Interfaces.FileStorage;
using ProSphere.Jobs.Documents.DeleteDocumentVerification;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProSphere.Features.Verification.Commands.AcceptFinancialInvestorVerification
{
    public class AcceptFinancialInvestorVerificationCommandHandler : IRequestHandler<AcceptFinancialInvestorVerificationCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSenderService _emailSenderService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AcceptFinancialInvestorVerificationCommandHandler(
            IUnitOfWork unitOfWork, IEmailSenderService emailSenderService,
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _emailSenderService = emailSenderService;
            _userManager = userManager;
        }

        public async Task<Result> Handle(AcceptFinancialInvestorVerificationCommand command, CancellationToken cancellationToken)
        {
            var isModeratorExist = await _userManager.FindByIdAsync(command.moderatorId) != null;
            if (!isModeratorExist)
                return Result.Failure("Moderator Not Found", StatusCodes.Status404NotFound);

            var financialDocument = await _unitOfWork.FinancialVerifications.GetByIdAsync(command.financialDocumentId);
            if (financialDocument == null || financialDocument.status == Status.Rejected || financialDocument.status == Status.Approved)
                return Result.Failure("Document Not Exist", StatusCodes.Status404NotFound);

            financialDocument.status = Status.Approved;
            financialDocument.ReviewedAt = DateTime.UtcNow;
            financialDocument.ReviewedBy = command.moderatorId;

            var investor = await _unitOfWork.Investors.FirstOrDefaultAsync(i => i.Id ==  financialDocument.InvestorId);
            if(investor is not null) investor.InvestorLevel = InvestorLevel.Financial;

            var userData = await _userManager.FindByIdAsync(financialDocument.InvestorId);

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Result.Failure("Error: Another Moderator Took A Verification Action" ,StatusCodes.Status409Conflict);
            }

            BackgroundJob.Enqueue<IDeleteDocumentVerificationJob>(
                service => service.DeleteFinancialVerificationDocuments(financialDocument.InvestorId, Status.Rejected)
                );

            _emailSenderService.SendAcceptanceVerifiedFinancialMail(userData!.Email!, userData.FirstName, userData.LastName);

            return Result.Success("Financial Is Verified Successfully");
        }
    }
}
