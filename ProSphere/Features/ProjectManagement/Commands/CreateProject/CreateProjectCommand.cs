using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectManagement.Commands.CreateProject
{
    public record CreateProjectCommand(string creatorId, CreateProjectRequest request) : IRequest<Result>
    {
    }
}
