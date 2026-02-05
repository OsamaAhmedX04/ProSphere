using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Authentication.Commands.ResendResetPasswordToken
{
    public record ResendResetPasswordTokenCommand(string email) : IRequest<Result>
    {
    }
}
