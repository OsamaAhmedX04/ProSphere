using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Employee.Commands.AssignToModerator
{
    public record AssignToModeratorCommand(AssignToModeratorRequest request) : IRequest<Result>
    {
    }
}
