using FluentValidation;

namespace ProSphere.Features.Moderator.Commands.CreateModerator
{
    public class CreateModeratorValidator : AbstractValidator<CreateModeratorRequest>
    {
        public CreateModeratorValidator()
        {
            RuleFor(m => m.Email)
                .NotEmpty().WithMessage("Email Is Required")
                .EmailAddress().WithMessage("Invalid Email");
        }
    }
}
