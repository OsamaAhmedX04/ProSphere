using FluentValidation;

namespace ProSphere.Features.Authentication.Commands.ResetPassword
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordValidator()
        {
            RuleFor(rp => rp.NewPassword)
                .NotEmpty().WithMessage("Password Is Required");
        }
    }
}
