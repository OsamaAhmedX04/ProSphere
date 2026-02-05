using FluentValidation;

namespace ProSphere.Features.Authentication.Commands.ChangePassword
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordValidator()
        {
            RuleFor(cp => cp.UserId)
                .NotEmpty().WithMessage("User Id Is Required");

            RuleFor(cp => cp.CurrentPassword)
                .NotEmpty().WithMessage("Current Password Is Required");

            RuleFor(cp => cp.NewPassword)
                .NotEmpty().WithMessage("New Password Is Required");
        }
    }
}
