using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectManagement.Commands.DeleteProject
{
    public record DeleteProjectCommand(string creatorId, Guid projectId) : IRequest<Result>
    {
    }
}
