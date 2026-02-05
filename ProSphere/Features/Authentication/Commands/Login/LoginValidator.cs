using FluentValidation;

namespace ProSphere.Features.Authentication.Commands.Login
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(l => l.Email)
                .NotEmpty().WithMessage("Email Is Required")
                .EmailAddress().WithMessage("Please Enter Correct Email Address");

            RuleFor(l => l.Password)
                .NotEmpty().WithMessage("Password Is Required");
        }
    }
}
