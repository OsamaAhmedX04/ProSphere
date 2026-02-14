using FluentValidation;

namespace ProSphere.Features.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeRequest>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Name Is Required")
                .MaximumLength(40).WithMessage("Max Length Of Name Is 40 Letter");

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email Is Required")
                .EmailAddress().WithMessage("InValid Email");

            RuleFor(e => e.Phone)
                .NotEmpty().WithMessage("Phone Is Required");

            RuleFor(e => e.Country)
                .NotEmpty().WithMessage("Counrty Name Is Required")
                .MaximumLength(50).WithMessage("Max Length Of Country Name Is 50 Letter");

            RuleFor(e => e.AssignedToModeratorId)
                .NotEmpty().WithMessage("Assigning Employee To Moderator Account Is Required");

        }
    }
}
