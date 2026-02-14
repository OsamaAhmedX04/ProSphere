using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Account.Commands.UpdateCreatorAccount
{
    public record UpdateCreatorAccountCommand(string creatorId, UpdateCreatorAccountRequest request) : IRequest<Result>
    {
    }
}
