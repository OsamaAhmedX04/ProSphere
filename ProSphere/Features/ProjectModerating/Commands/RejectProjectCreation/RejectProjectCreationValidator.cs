using FluentValidation;

namespace ProSphere.Features.ProjectModerating.Commands.RejectProjectCreation
{
    public class RejectProjectCreationValidator : AbstractValidator<RejectProjectCreationRequest>
    {
        public RejectProjectCreationValidator()
        {
            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("Reason Of Rejection Is Required")
                .MaximumLength(1000).WithMessage("Max Length Of Reason Of Rejection Is 1000 char");
        }
    }
}
