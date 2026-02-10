using FluentValidation;
using MediatR;
using ProSphere.Domain.Entities;
using ProSphere.Extensions;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.CreatorSkills.Commands.AddNewSkills
{
    public class AddNewSkillsCommandHandler : IRequestHandler<AddNewSkillsCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<AddNewSkillsRequest> _validator;

        public AddNewSkillsCommandHandler(IUnitOfWork unitOfWork, IValidator<AddNewSkillsRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result> Handle(AddNewSkillsCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command.request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }

            var skillList = command.request.SkillsId
                .Distinct()
                .Select(skillId => new CreatorSkill
                {
                    CreatorId = command.CreatorId,
                    SkillId = skillId
                }).ToList();

            await _unitOfWork.CreatorSkills.AddRangeAsync(skillList);
            await _unitOfWork.CompleteAsync();

            return Result.Success("Skills Added Successfully");
        }
    }
}
