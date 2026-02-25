using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectManagement.Commands.UpdateProject
{
    public record UpdateProjectCommand(string creatorId, Guid projectId, UpdateProjectRequest request) : IRequest<Result>
    {
    }
}
