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

            var project = await _unitOfWork.Projects.FirstOrDefaultAsync(p => p.Id == command.projectId && p.IsActive == true);
            if (project is null) return Result.Failure("Project Not Found", StatusCodes.Status404NotFound);

            if(project.CreatorId == command.creatorId) return Result.Failure("Can't Vote For Your Project", StatusCodes.Status400BadRequest);

            var oldVote = await _unitOfWork.ProjectsVotes
                .FirstOrDefaultAsync(v => v.ProjectId == command.projectId && v.CreatorId == command.creatorId);
            if(oldVote is not null) return Result.Failure("This Creator had Already Voted On This Project", StatusCodes.Status400BadRequest);


            var vote = new ProjectVote { CreatorId = command.creatorId, ProjectId = command.projectId };
            await _unitOfWork.ProjectsVotes.AddAsync(vote);
            await _unitOfWork.CompleteAsync();

            return Result.Success("Vote Added Successfully");
        }
    }
}
