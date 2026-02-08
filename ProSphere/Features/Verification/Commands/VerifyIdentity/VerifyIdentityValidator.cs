using FluentValidation;
using ProSphere.Domain.Constants;

namespace ProSphere.Features.Verification.Commands.VerifyIdentity
{
    public class VerifyIdentityValidator : AbstractValidator<VerifyIdentityRequest>
    {
        public VerifyIdentityValidator()
        {
            RuleFor(v => v.IdFrontImage)
                .NotNull().WithMessage("Id Front Image Is Required")
                .Must(image => image.Length > 0).WithMessage("Invalid Image File, No Content")
                .Must(image => image.Length <= FileRestriction.AllowableImageFileSize)
                    .WithMessage("Image size must be less than 1 MB.")
                .Must(image => FileRestriction.AllowableImageExtensions.Contains(Path.GetExtension(image.FileName)))
                    .WithMessage($"Invalid image format. Allowed formats are: {string.Join(',', FileRestriction.AllowableImageExtensions)}");

            RuleFor(v => v.IdBackImage)
                .NotNull().WithMessage("Id Back Image Is Required")
                .Must(image => image.Length > 0).WithMessage("Invalid Image File , No Content")
                .Must(image => image.Length <= FileRestriction.AllowableImageFileSize)
                    .WithMessage("Image size must be less than 1 MB.")
                .Must(image => FileRestriction.AllowableImageExtensions.Contains(Path.GetExtension(image.FileName)))
                    .WithMessage($"Invalid image format. Allowed formats are: {string.Join(',', FileRestriction.AllowableImageExtensions)}");

            RuleFor(v => v.SelfieWithId)
                .NotNull().WithMessage("Selfie Image With Id Is Required")
                .Must(image => image.Length > 0).WithMessage("Invalid Image File , No Content")
                .Must(image => image.Length <= FileRestriction.AllowableImageFileSize)
                    .WithMessage("Image size must be less than 1 MB.")
                .Must(image => FileRestriction.AllowableImageExtensions.Contains(Path.GetExtension(image.FileName)))
                    .WithMessage($"Invalid image format. Allowed formats are: {string.Join(',', FileRestriction.AllowableImageExtensions)}");
        }
    }
}
