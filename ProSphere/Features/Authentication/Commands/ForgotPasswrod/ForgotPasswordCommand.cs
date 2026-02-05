using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Authentication.Commands.ForgotPasswrod
{
    public record ForgotPasswordCommand(string email) : IRequest<Result>
    {
    }
}
