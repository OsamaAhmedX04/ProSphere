using FluentValidation;
using ProSphere.Domain.Constants.FileConstants;

namespace ProSphere.Features.CV.Commands.UploadCV
{
    public class UploadCVValidator : AbstractValidator<UploadCVRequest>
    {
        public UploadCVValidator()
        {
            RuleFor(u => u.CV)
                .Must(file => file.Length < FileRestriction.AllowableCVFileSize)
                    .WithMessage("File size must be less than or equal 3 MB.")
                .Must(file => FileRestriction.AllowableCVFileExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
                    .WithMessage($"Invalid image format. Allowed formats are: {string.Join(',', FileRestriction.AllowableImageExtensions)}")
                    .When(u => u.CV is not null);
        }
    }
}
