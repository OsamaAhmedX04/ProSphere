using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Account.Commands.RequestDeleteAccount
{
    public record RequestDeleteAccountCommand(string userId) : IRequest<Result>
    {
    }
}
