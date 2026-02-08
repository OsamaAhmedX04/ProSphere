using FluentValidation;
using ProSphere.Domain.Constants;

namespace ProSphere.Features.Verification.Commands.VerifyFinancialInvestor
{
    public class VerifyFinancialInvestorValidator : AbstractValidator<VerifyFinancialInvestorRequest>
    {
        public VerifyFinancialInvestorValidator()
        {
            RuleFor(v => v.DocumentType)
                .NotEmpty().WithMessage("Document Type Is Required")
                .Must(d =>
                    d.ToLower() == FinancialType.FinancialLetter.ToLower()
                    ||
                    d.ToLower() == FinancialType.Wallet.ToLower()
                    ||
                    d.ToLower() == FinancialType.BankStatement.ToLower()
                    ||
                    d.ToLower() == FinancialType.Other.ToLower()
                    ).WithMessage("Please Enter Valid Finincial Document Type");

            RuleFor(v => v.DocumentImage)
                .NotNull().WithMessage("Document Image Is Required")
                .Must(image => image.Length > 0)
                    .WithMessage("Invalid Image File , No Content")
                .Must(image => image.Length < FileRestriction.AllowableImageFileSize)
                    .WithMessage("Image size must be less than 1 MB.")
                .Must(image => FileRestriction.AllowableImageExtensions.Contains(Path.GetExtension(image.FileName)))
                    .WithMessage($"Invalid image format. Allowed formats are: {string.Join(',', FileRestriction.AllowableImageExtensions)}");

            RuleFor(v => v.Notes)
                .MaximumLength(500).WithMessage("Max Length Of Noted Is 500")
                .When(v => !string.IsNullOrEmpty(v.Notes));
        }
    }
}
