using FluentValidation;
using ProSphere.Domain.Constants;
using ProSphere.Domain.Enums;

namespace ProSphere.Features.Registration.Commands.Register
{
    public class RegisterValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterValidator()
        {
            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("First Name Is Required")
                .MaximumLength(30).WithMessage("First Name Should Not Excced 30 Letter");

            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage("Last Name Is Required")
                .MaximumLength(30).WithMessage("Last Name Should Not Excced 30 Letter");

            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Email Is Required")
                .EmailAddress().WithMessage("Please Enter Correct Email Address");

            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Password Is Required");

            RuleFor(r => r.Gender)
                .NotEmpty().WithMessage("Gender Is Required")
                .Must(g => g.ToLower() == Gender.Male.ToString().ToLower() || g.ToLower() == Gender.Female.ToString().ToLower());

            RuleFor(r => r.Role)
                .NotEmpty().WithMessage("Role Is Required")
                .Must(r => r.ToLower() == Role.Creator.ToLower() || r.ToLower() == Role.Investor.ToLower());
        }
    }

}
