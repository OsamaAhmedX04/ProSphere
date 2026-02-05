using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Authentication.Commands.Register
{
    public record RegisterCommand(RegisterRequest request) : IRequest<Result>
    {
    }
}
