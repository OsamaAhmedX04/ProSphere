using FluentValidation;
using ProSphere.Domain.Constants.FileConstants;

namespace ProSphere.Features.Verification.Commands.VerifyProfessionalInvestor
{
    public class VerifyProfessionalInvestorValidator : AbstractValidator<VerifyProfessionalInvestorRequest>
    {
        public VerifyProfessionalInvestorValidator()
        {
            RuleFor(v => v.DocumentTypeId)
                .NotEmpty().WithMessage("Document Type Id Is Required");

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
