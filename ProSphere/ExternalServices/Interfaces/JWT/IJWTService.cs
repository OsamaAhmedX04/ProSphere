using ProSphere.Shared.DTOs.Authentication;

namespace ProSphere.ExternalServices.Interfaces.JWT
{
    public interface IJWTService
    {
        string GenerateToken(AuthenticatedUserDto user);
    }
}
