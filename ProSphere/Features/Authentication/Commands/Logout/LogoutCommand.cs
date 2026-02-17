using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Authentication.Commands.Logout
{
    public record LogoutCommand : IRequest<Result>
    {
    }
}
