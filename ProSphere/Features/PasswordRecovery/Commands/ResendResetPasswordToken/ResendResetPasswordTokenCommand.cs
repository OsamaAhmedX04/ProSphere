using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.PasswordRecovery.Commands.ResendResetPasswordToken
{
    public record ResendResetPasswordTokenCommand(string email) : IRequest<Result>
    {
    }
}
