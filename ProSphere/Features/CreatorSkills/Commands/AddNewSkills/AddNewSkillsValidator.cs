using FluentValidation;

namespace ProSphere.Features.CreatorSkills.Commands.AddNewSkills
{
    public class AddNewSkillsValidator : AbstractValidator<AddNewSkillsRequest>
    {
        public AddNewSkillsValidator()
        {
            RuleForEach(s => s.SkillsId)
                .NotEmpty().WithMessage("Skill ID Cannot Be Empty");
        }
    }
}
