using FluentValidation;

namespace ProSphere.Features.Admin.Commands.CreateAdmin
{
    public class CreateAdminValidator : AbstractValidator<CreateAdminRequest>
    {
        public CreateAdminValidator()
        {
            RuleFor(a => a.FirstName)
                .NotEmpty().WithMessage("First Name Is Required")
                .MaximumLength(40).WithMessage("First Name Must Not Excced 40 Chars");

            RuleFor(a => a.LastName)
                .NotEmpty().WithMessage("Last Name Is Required")
                .MaximumLength(40).WithMessage("Lasr Name Must Not Excced 40 Chars");

            RuleFor(a => a.Email)
                .NotEmpty().WithMessage("Email Is Required")
                .EmailAddress().WithMessage("InValid Email");
        }
    }
}
