using FluentValidation;
using MediatR;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.Extensions;
using ProSphere.ExternalServices.Interfaces.FileStorage;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Verification.Commands.VerifyFinancialInvestor
{
    public class VerifyFinancialInvestorCommandHandler : IRequestHandler<VerifyFinancialInvestorCommand, Result>
    {
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<VerifyFinancialInvestorRequest> _validator;

        public VerifyFinancialInvestorCommandHandler(IFileService fileService, IUnitOfWork unitOfWork, IValidator<VerifyFinancialInvestorRequest> validator)
        {
            _fileService = fileService;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result> Handle(VerifyFinancialInvestorCommand command, CancellationToken cancellationToken)
        {
            var isTherePendingDocs = await _unitOfWork.FinancialVerifications
                .FirstOrDefaultAsync(v => v.InvestorId == command.investorId && v.status == Status.Pending) != null;
            if (isTherePendingDocs)
                return Result.Failure("You Already Have Uploaded You Financial Verification And Its Under Revision", 400);

            var validationResult = await _validator.ValidateAsync(command.request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }

            var investor = await _unitOfWork.Investors.GetEnhancedAsync(
                filter: i => i.Id == command.investorId,
                selector: i => new
                {
                    investorId = i.Id,
                    IsIdentityVerified = i.User.IsVerified
                });

            if (investor == null)
                return Result.Failure("Investor Not Found", StatusCodes.Status404NotFound);
            if (!investor.IsIdentityVerified)
                return Result.Failure("You Have to Verify Your Identity First", StatusCodes.Status400BadRequest);


            var documentImageURL = await _fileService.UploadAsync(command.request.DocumentImage, $"Verifications/Financial/{command.investorId}");

            var financialVerification = new FinancialVerification
            {
                InvestorId = command.investorId,
                DocumentType = command.request.DocumentType,
                DocumentURL = documentImageURL,
                Notes = command.request.Notes != null ? command.request.Notes : null,
                status = Status.Pending,
                CreatedAt = DateTime.UtcNow,
            };

            await _unitOfWork.FinancialVerifications.AddAsync(financialVerification);
            await _unitOfWork.CompleteAsync();

            return Result.Success("Financial Verification Documents Have Been Sent Successfully And It's Now Under Revision");
        }
    }
}
