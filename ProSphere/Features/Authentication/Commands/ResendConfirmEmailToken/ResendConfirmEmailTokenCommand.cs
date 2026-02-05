using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Authentication.Commands.ResendConfirmEmailToken
{
    public record ResendConfirmEmailTokenCommand(string email) : IRequest<Result>
    {
    }
}
