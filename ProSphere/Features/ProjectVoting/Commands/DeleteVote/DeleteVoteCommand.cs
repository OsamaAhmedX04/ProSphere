using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectVoting.Commands.DeleteVote
{
    public record DeleteVoteCommand(string creatorId, Guid projectId) : IRequest<Result>
    {
    }
}
