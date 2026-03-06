using FluentValidation;
using ProSphere.Domain.Constants.FileConstants;

namespace ProSphere.Features.Chat.Commands.SendMessage
{
    public class SendMessageValidator : AbstractValidator<SendMessageRequest>
    {
        public SendMessageValidator()
        {
            RuleFor(x => x)
                .Must(x => !string.IsNullOrWhiteSpace(x.Message) || x.Image != null)
                .WithMessage("You must send either a message or an image.");

            RuleFor(x => x.Message)
                .MaximumLength(2000).WithMessage($"Max Length Of Message Is 2000")
                .When(x => !string.IsNullOrWhiteSpace(x.Message));

            RuleFor(v => v.Image)
                .Must(image => image.Length < FileRestriction.AllowableImageFileSize)
                    .WithMessage("Image size must be less than 1 MB.")
                .Must(image => FileRestriction.AllowableImageExtensions.Contains(Path.GetExtension(image.FileName)))
                    .WithMessage($"Invalid image format. Allowed formats are: {string.Join(',', FileRestriction.AllowableImageExtensions)}")
                .When(x => x.Image != null);
        }
    }
}
