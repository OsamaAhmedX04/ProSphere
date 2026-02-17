using FluentValidation;

namespace ProSphere.Features.Employee.Commands.AssignToModerator
{
    public class AssignToModeratorValidator : AbstractValidator<AssignToModeratorRequest>
    {
        public AssignToModeratorValidator()
        {

            RuleFor(x => x.EmployeeId)
                .NotEmpty().WithMessage("Employee Id Is Required");

            RuleFor(x => x.ModeratorId)
                .NotEmpty().WithMessage("Moderator Id Is Required");
        }
    }
}
