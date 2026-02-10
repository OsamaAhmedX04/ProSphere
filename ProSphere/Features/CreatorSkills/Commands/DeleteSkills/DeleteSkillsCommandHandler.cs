using FluentValidation;
using MediatR;
using ProSphere.Extensions;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.CreatorSkills.Commands.DeleteSkills
{
    public class DeleteSkillsCommandHandler : IRequestHandler<DeleteSkillsCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<DeleteSkillsRequest> _validator;

        public DeleteSkillsCommandHandler(IUnitOfWork unitOfWork, IValidator<DeleteSkillsRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result> Handle(DeleteSkillsCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command.request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }

            var skillList = command.request.SkillsId.Distinct();

            await _unitOfWork.CreatorSkills.BulkDeleteAsync(cs => cs.CreatorId == command.creatorId && skillList.Contains(cs.SkillId));

            return Result.Success("Skills Deleted Successfully.");
        }
    }
}
