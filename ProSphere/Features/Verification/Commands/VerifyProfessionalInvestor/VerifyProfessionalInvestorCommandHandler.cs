using FluentValidation;
using MediatR;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.Extensions;
using ProSphere.ExternalServices.Interfaces.FileStorage;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Verification.Commands.VerifyProfessionalInvestor
{
    public class VerifyProfessionalInvestorCommandHandler : IRequestHandler<VerifyProfessionalInvestorCommand, Result>
    {
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<VerifyProfessionalInvestorRequest> _validator;

        public VerifyProfessionalInvestorCommandHandler(
            IFileService fileService, IUnitOfWork unitOfWork,
            IValidator<VerifyProfessionalInvestorRequest> validator)
        {
            _fileService = fileService;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result> Handle(VerifyProfessionalInvestorCommand command, CancellationToken cancellationToken)
        {
            var isTherePendingDocs = await _unitOfWork.ProfessionalVerifications
                .FirstOrDefaultAsync(v => v.InvestorId == command.investorId && v.status == Status.Pending) != null;
            if (isTherePendingDocs)
                return Result.Failure("You Already Have Uploaded You Professional Verification And Its Under Revision", 400);

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
                    IsFinancialVerified = i.InvestorLevel == InvestorLevel.Financial,
                });

            if (investor == null)
                return Result.Failure("Investor Not Found", StatusCodes.Status404NotFound);
            if (!investor.IsFinancialVerified)
                return Result.Failure("You Have to Verify That You are a Financial Investor First", StatusCodes.Status400BadRequest);

            var documentTypeExists = await _unitOfWork.ProfessionalDocumentTypes
                .IsExistAsync(command.request.DocumentTypeId);
            if (!documentTypeExists)
                return Result.Failure("Document Type Not Found", StatusCodes.Status404NotFound);

            var documentImageURL = await _fileService.UploadAsync(command.request.DocumentImage, $"Verifications/Professional/{command.investorId}");

            var professionalVerification = new ProfessionalVerification
            {
                InvestorId = command.investorId,
                DocumentTypeId = command.request.DocumentTypeId,
                DocumentURL = documentImageURL,
                Notes = command.request.Notes != null ? command.request.Notes : null,
                status = Status.Pending,
                CreatedAt = DateTime.UtcNow,
            };

            await _unitOfWork.ProfessionalVerifications.AddAsync(professionalVerification);
            await _unitOfWork.CompleteAsync();

            return Result.Success("Professional Verification Documents Have Been Sent Successfully And It's Now Under Revision");
        }
    }
}
