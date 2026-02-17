using FluentValidation;
using ProSphere.Domain.Constants.FileConstants;

namespace ProSphere.Features.Account.Commands.UpdateCreatorAccount
{
    public class UpdateCreatorAccountValidator : AbstractValidator<UpdateCreatorAccountRequest>
    {
        public UpdateCreatorAccountValidator()
        {
            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("First Name Is Required")
                .MaximumLength(30).WithMessage("First Name Should Not Excced 30 Letter");

            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage("Last Name Is Required")
                .MaximumLength(30).WithMessage("Last Name Should Not Excced 30 Letter");

            RuleFor(x => x.Username)
           .NotEmpty().WithMessage("Username is required")
           .Matches(@"^[a-zA-Z0-9._]{3,20}$")
           .WithMessage("Username can only contain letters, numbers, dot, underscore and must be 3-20 characters long");

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
                .MaximumLength(100).WithMessage("Max Length Of Tech Stack Is 100")
                .When(u => !string.IsNullOrEmpty(u.HeadLine));
        }
    }
}
