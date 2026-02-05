using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Authentication.Commands.ChangePassword
{
    public record ChangePasswordCommand(ChangePasswordRequest request) : IRequest<Result>
    {
    }
}
