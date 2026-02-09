using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Moderator.Commands.RecycleModeratorAccount
{
    public record RecycleModeratorAccountCommand(string moderatorId) : IRequest<Result<RecycleModeratorAccountResponse>>
    {
    }
}
