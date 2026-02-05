using ProSphere.Domain.Entities;
using ProSphere.Shared.DTOs.Authentication;

namespace ProSphere.ExternalServices.Interfaces.Authentication
{
    public interface IAuthenticationTokenService
    {
        Task<AuthenticationTokenDto> GenerateAuthenticationTokens(ApplicationUser user, bool rememberMe);
        Task<AuthenticationTokenDto> GenerateAuthenticationTokens(ApplicationUser user);
    }
}
