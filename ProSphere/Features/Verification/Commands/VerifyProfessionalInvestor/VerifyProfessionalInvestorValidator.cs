using FluentValidation;
using ProSphere.Domain.Constants;

namespace ProSphere.Features.Verification.Commands.VerifyProfessionalInvestor
{
    public class VerifyProfessionalInvestorValidator : AbstractValidator<VerifyProfessionalInvestorRequest>
    {
        public VerifyProfessionalInvestorValidator()
        {
            RuleFor(v => v.DocumentType)
                .NotEmpty().WithMessage("Document Type Is Required")
                .Must(d =>
                    d.ToLower() == ProfessionalType.CommercialRegister.ToLower()
                    ||
                    d.ToLower() == ProfessionalType.Letter.ToLower()
                    ||
                    d.ToLower() == ProfessionalType.Membership.ToLower()
                    ||
                    d.ToLower() == ProfessionalType.TaxCard.ToLower()
                    ||
                    d.ToLower() == ProfessionalType.Other.ToLower()
                    ).WithMessage("Please Enter Valid Professional Document Type");

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
