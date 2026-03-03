using MediatR;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectVoting.Commands.DeleteVote
{
    public class DeleteVoteCommandHandler : IRequestHandler<DeleteVoteCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteVoteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteVoteCommand command, CancellationToken cancellationToken)
        {
            var isCreatorExist = await _unitOfWork.Creators.IsExistAsync(command.creatorId);
            if (!isCreatorExist) return Result.Failure("Creator Not Found", StatusCodes.Status404NotFound);

            var project = await _unitOfWork.Projects.FirstOrDefaultAsync(p => p.Id == command.projectId && p.IsActive == true);
            if (project is null) return Result.Failure("Project Not Found", StatusCodes.Status404NotFound);


            await _unitOfWork.ProjectsVotes.DeleteAsync(vote =>
                                                        vote.CreatorId == command.creatorId && vote.ProjectId == command.projectId);
            await _unitOfWork.CompleteAsync();

            return Result.Success("Vote Removed Successfully");
        }
    }
}
