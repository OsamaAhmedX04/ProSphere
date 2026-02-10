using MediatR;
using ProSphere.ResultResponse;
using ProSphere.Shared.DTOs.Authentication;

namespace ProSphere.Features.PasswordManagement.Commands.ChangeInActiveRolePassword
{
    public record ChangeInActiveRolePasswordCommand(ChangeInActiveRolePasswordRequest request)
        : IRequest<Result<AuthenticationTokenDto>>
    {
    }
}
