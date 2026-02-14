using FluentValidation;
using ProSphere.Domain.Constants;

namespace ProSphere.Features.Account.Commands.UpdateInvestorAccount
{
    public class UpdateInvestorAccountValidator : AbstractValidator<UpdateInvestorAccountRequest>
    {
        public UpdateInvestorAccountValidator()
        {
            RuleFor(u => u.ImageProfile)
                .Must(image => image.Length < FileRestriction.AllowableImageFileSize)
                    .WithMessage("Image size must be less than 1 MB.")
                .Must(image => FileRestriction.AllowableImageExtensions.Contains(Path.GetExtension(image.FileName)))
                    .WithMessage($"Invalid image format. Allowed formats are: {string.Join(',', FileRestriction.AllowableImageExtensions)}")
                    .When(u => u.ImageProfile is not null);

            RuleFor(u => u.BIO)
                .MaximumLength(500).WithMessage("Max Length Of BIO Is 500")
                .When(u => !string.IsNullOrEmpty(u.BIO));

            RuleFor(u => u.HeadLine)
                .MaximumLength(100).WithMessage("Max Length Of Brief Is 100")
                .When(u => !string.IsNullOrEmpty(u.HeadLine));
        }
    }
}
