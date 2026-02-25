using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectModerating.Commands.RejectProjectCreation
{
    public record RejectProjectCreationCommand(string moderatorId, Guid projectId, RejectProjectCreationRequest request) : IRequest<Result>
    {
    }
}
