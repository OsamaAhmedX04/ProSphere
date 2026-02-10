using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Registration.Commands.Register
{
    public record RegisterCommand(RegisterRequest request) : IRequest<Result>
    {
    }
}
