using MediatR;
using ProSphere.Features.Authentication.Commands.Login;
using ProSphere.ResultResponse;
using ProSphere.Shared.DTOs;

namespace ProSphere.Features.Authentication.Commands.ConfirmEmail
{
    public record ConfirmEmailCommand(string userId, string token) : IRequest<Result<AuthenticationTokenDto>>
    {
    }
}
