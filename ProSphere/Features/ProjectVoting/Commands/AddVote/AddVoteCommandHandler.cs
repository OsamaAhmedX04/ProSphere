using MediatR;
using ProSphere.Domain.Entities;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectVoting.Commands.AddVote
{
    public class AddVoteCommandHandler : IRequestHandler<AddVoteCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddVoteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddVoteCommand command, CancellationToken cancellationToken)
        {
            var isCreatorExist = await _unitOfWork.Creators.IsExistAsync(command.creatorId);
            if (!isCreatorExist) return Result.Failure("Creator Not Found", StatusCodes.Status404NotFound);

            var project = await _unitOfWork.Projects.FirstOrDefaultAsync(p => p.Id == command.projectId);
            if (project is null) return Result.Failure("Project Not Found", StatusCodes.Status404NotFound);

            var vote = new ProjectVote { CreatorId = command.creatorId , ProjectId = command.projectId};
            await _unitOfWork.ProjectsVotes.AddAsync(vote);
            await _unitOfWork.CompleteAsync();

            return Result.Success("Vote Added Successfully");
        }
    }
}
