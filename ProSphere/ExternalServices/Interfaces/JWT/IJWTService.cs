using ProSphere.Shared.DTOs;

namespace ProSphere.ExternalServices.Interfaces.JWT
{
    public interface IJWTService
    {
        string GenerateToken(AuthenticatedUserDto user);
    }
}
