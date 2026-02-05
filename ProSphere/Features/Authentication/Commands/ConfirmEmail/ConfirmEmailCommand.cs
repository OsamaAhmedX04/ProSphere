using MediatR;
using ProSphere.ResultResponse;
using ProSphere.Shared.DTOs.Authentication;

namespace ProSphere.Features.Authentication.Commands.ConfirmEmail
{
    public record ConfirmEmailCommand(string userId, string token) : IRequest<Result<AuthenticationTokenDto>>
    {
    }
}
