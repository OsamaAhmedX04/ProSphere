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

namespace ProSphere.Features.Verification.Commands.RejectFinancialInvestorVerification
{
    public class RejectFinancialInvestorVerificationCommandHandler : IRequestHandler<RejectFinancialInvestorVerificationCommand, Result>
    {
        private readonly IValidator<RejectFinancialInvestorVerificationRequest> _validator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSenderService _emailSenderService;

        public RejectFinancialInvestorVerificationCommandHandler(
            IValidator<RejectFinancialInvestorVerificationRequest> validator, IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager, IEmailSenderService emailSenderService)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _emailSenderService = emailSenderService;
        }

        public async Task<Result> Handle(RejectFinancialInvestorVerificationCommand command, CancellationToken cancellationToken)
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

            var financialDocument = await _unitOfWork.FinancialVerifications.GetByIdAsync(command.financialDocumentId);
            if (financialDocument == null)
                return Result.Failure("Document Not Exist", StatusCodes.Status404NotFound);

            financialDocument.status = Status.Rejected;
            financialDocument.ReviewedBy = command.moderatorId;
            financialDocument.ReviewedAt = DateTime.UtcNow;
            financialDocument.RejectionReason = command.request.RejectReason;

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Result.Failure("Error: Another Moderator Took A Verification Action", StatusCodes.Status409Conflict);
            }

            var user = await _userManager.FindByIdAsync(financialDocument.InvestorId);

            _emailSenderService.SendRejectionVerifiedFinancialMail(user!.Email!, user.FirstName, user.LastName, command.request.RejectReason);

            return Result.Success("Financail Is Rejected Successfully");
        }
    }
}
