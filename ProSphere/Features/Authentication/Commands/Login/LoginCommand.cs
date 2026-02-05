using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Authentication.Commands.Login
{
    public record LoginCommand(LoginRequest request) : IRequest<Result<LoginResponse>>
    {
    }
}
