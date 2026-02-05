using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Authentication.Commands.DeleteAccount
{
    public record RequestDeleteAccountCommand(string userId) : IRequest<Result>
    {
    }
}
