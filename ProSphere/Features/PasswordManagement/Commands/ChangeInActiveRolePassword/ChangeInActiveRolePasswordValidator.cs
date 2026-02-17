using FluentValidation;

namespace ProSphere.Features.PasswordManagement.Commands.ChangeInActiveRolePassword
{
    public class ChangeInActiveRolePasswordValidator : AbstractValidator<ChangeInActiveRolePasswordRequest>
    {
        public ChangeInActiveRolePasswordValidator()
        {
            RuleFor(cp => cp.Email)
                .NotEmpty().WithMessage("User Id Is Required")
                .EmailAddress().WithMessage("InValid Email");

            RuleFor(cp => cp.CurrentPassword)
                .NotEmpty().WithMessage("Current Password Is Required");

            RuleFor(cp => cp.NewPassword)
                .NotEmpty().WithMessage("New Password Is Required");
        }
    }
}
