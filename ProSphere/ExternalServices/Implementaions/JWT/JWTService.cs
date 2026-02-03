using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProSphere.Options;
using ProSphere.Shared.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProSphere.ExternalServices.Implementaions.JWT
{
    public class JWTService
    {
        //private readonly IConfiguration _configuration;

        private readonly JWTOptions _options;
        public JWTService(IOptions<JWTOptions> options)
        {
            //_configuration = configuration;
            _options = options.Value;
        }

        public string GenerateToken(AuthenticatedUserDto user)
        {
            var claims = new[]
            {
                new Claim("UserId", user.Id),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                //issuer: _configuration["JWT:Issuer"],
                //audience: _configuration["JWT:Audience"],
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                //expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JWT:DurationInMinutes"])),
                expires: DateTime.UtcNow.AddMinutes(_options.DurationInMinutes),
                signingCredentials: creds);

            var jwToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwToken;
        }
    }
}
