using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Registration.Commands.ResendConfirmEmailToken
{
    public record ResendConfirmEmailTokenCommand(string email) : IRequest<Result>
    {
    }
}
