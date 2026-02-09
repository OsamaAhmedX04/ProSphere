using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Moderator.Commands.CreateModerator
{
    public record CreateModeratorCommand(CreateModeratorRequest request) : IRequest<Result<CreateModeratorResponse>>
    {
    }
}
