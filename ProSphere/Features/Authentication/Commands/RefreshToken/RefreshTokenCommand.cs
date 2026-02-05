using MediatR;
using ProSphere.ResultResponse;
using ProSphere.Shared.DTOs.Authentication;

namespace ProSphere.Features.Authentication.Commands.RefreshToken
{
    public record RefreshTokenCommand(RefreshTokenRequest request) : IRequest<Result<AuthenticationTokenDto>>
    {
    }
}
