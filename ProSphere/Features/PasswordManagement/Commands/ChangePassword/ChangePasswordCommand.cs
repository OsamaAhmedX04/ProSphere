using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.PasswordManagement.Commands.ChangePassword
{
    public record ChangePasswordCommand(ChangePasswordRequest request) : IRequest<Result>
    {
    }
}
