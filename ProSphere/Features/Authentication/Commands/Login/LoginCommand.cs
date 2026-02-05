using MediatR;
using ProSphere.ResultResponse;
using ProSphere.Shared.DTOs.Authentication;

namespace ProSphere.Features.Authentication.Commands.Login
{
    public record LoginCommand(LoginRequest request) : IRequest<Result<AuthenticationTokenDto>>
    {
    }
}
