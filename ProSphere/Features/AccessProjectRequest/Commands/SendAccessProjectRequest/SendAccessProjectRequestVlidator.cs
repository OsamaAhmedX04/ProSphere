using FluentValidation;

namespace ProSphere.Features.AccessProjectRequest.Commands.SendAccessProjectRequest
{
    public class SendAccessProjectRequestVlidator : AbstractValidator<SendAccessProjectRequestRequest>
    {
        public SendAccessProjectRequestVlidator()
        {
            RuleFor(x => x.Message)
                .MaximumLength(300).WithMessage("Max Length Of Message Is 300 Letter")
                .When(x => x.Message is not null);
        }
    }
}
