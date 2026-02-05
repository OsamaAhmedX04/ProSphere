using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Authentication.Commands.ResetPassword
{
    public record ResetPasswordCommand(string userId, string token, ResetPasswordRequest request) : IRequest<Result>
    {
    }
}
