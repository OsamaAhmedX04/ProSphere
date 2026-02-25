using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectModerating.Commands.AcceptProjectCreation
{
    public record AcceptProjectCreationCommand(string moderatorId, Guid projectId) : IRequest<Result>
    {
    }
}
