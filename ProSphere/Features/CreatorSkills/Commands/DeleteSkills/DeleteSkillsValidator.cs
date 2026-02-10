using FluentValidation;

namespace ProSphere.Features.CreatorSkills.Commands.DeleteSkills
{
    public class DeleteSkillsValidator : AbstractValidator<DeleteSkillsRequest>
    {
        public DeleteSkillsValidator()
        {
            RuleForEach(s => s.SkillsId)
                .NotEmpty().WithMessage("Skill ID Cannot Be Empty");
        }
    }
}
