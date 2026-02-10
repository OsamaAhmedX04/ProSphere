using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.PasswordRecovery.Commands.ForgotPasswrod
{
    public record ForgotPasswordCommand(string email) : IRequest<Result>
    {
    }
}
