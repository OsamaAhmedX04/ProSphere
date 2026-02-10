using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.PasswordRecovery.Commands.ResetPassword
{
    public record ResetPasswordCommand(string userId, string token, ResetPasswordRequest request) : IRequest<Result>
    {
    }
}
