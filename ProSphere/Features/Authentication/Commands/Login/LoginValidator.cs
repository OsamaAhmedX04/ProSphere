using FluentValidation;

namespace ProSphere.Features.Authentication.Commands.Login
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(l => l.Email)
                .NotEmpty().WithMessage("Email Is Required");

            RuleFor(l => l.Email)
                .EmailAddress().WithMessage("Please Enter Correct Email Address")
                .When(l => l.Email.Contains("@"));

            RuleFor(l => l.Password)
                .NotEmpty().WithMessage("Password Is Required");
        }
    }
}
