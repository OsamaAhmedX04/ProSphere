using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectVoting.Commands.AddVote
{
    public record AddVoteCommand(string creatorId, Guid projectId) : IRequest<Result>
    {
    }
}
