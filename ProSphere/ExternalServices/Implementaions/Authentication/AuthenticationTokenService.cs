using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProSphere.Domain.Entities;
using ProSphere.ExternalServices.Interfaces.Authentication;
using ProSphere.ExternalServices.Interfaces.JWT;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.Shared.DTOs.Authentication;

namespace ProSphere.ExternalServices.Implementaions.Authentication
{
    public class AuthenticationTokenService : IAuthenticationTokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJWTService _jwtService;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationTokenService(UserManager<ApplicationUser> userManager, IJWTService jwtService, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthenticationTokenDto> GenerateAuthenticationTokens(ApplicationUser user, bool rememberMe)
        {
            var userRole = await _userManager.GetRolesAsync(user);
            var token = _jwtService.GenerateToken(new AuthenticatedUserDto
            {
                Id = user.Id,
                Role = userRole.First()
            });

            var refreshToken = Guid.NewGuid().ToString();

            var authenticationTokenResponse = new AuthenticationTokenDto
            {
                Token = token,
                RefreshToken = refreshToken,
            };

            var expirationRefreshTokenDate = rememberMe ? DateTime.UtcNow.AddMonths(3) : DateTime.UtcNow.AddHours(12);
            var tokenRow = await _unitOfWork.RefreshTokenAuths.GetAllAsIQueryable().FirstOrDefaultAsync(rt => rt.UserId == user.Id);
            if (tokenRow == null)
            {
                await _unitOfWork.RefreshTokenAuths.AddAsync(new RefreshTokenAuth
                {
                    UserId = user.Id,
                    Token = token,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiration = expirationRefreshTokenDate
                });
            }
            else
            {
                tokenRow.Token = token;
                tokenRow.RefreshToken = refreshToken;
                tokenRow.RefreshTokenExpiration = expirationRefreshTokenDate;
            }

            await _unitOfWork.CompleteAsync();

            return authenticationTokenResponse;
        }

        public async Task<AuthenticationTokenDto> GenerateAuthenticationTokens(ApplicationUser user)
        {
            var userRole = await _userManager.GetRolesAsync(user);
            var token = _jwtService.GenerateToken(new AuthenticatedUserDto
            {
                Id = user.Id,
                Role = userRole.First()
            });

            var refreshToken = Guid.NewGuid().ToString();

            var authenticationTokenResponse = new AuthenticationTokenDto
            {
                Token = token,
                RefreshToken = refreshToken,
            };

            var tokenRow = await _unitOfWork.RefreshTokenAuths.GetAllAsIQueryable().FirstOrDefaultAsync(rt => rt.UserId == user.Id);
            
            tokenRow!.Token = token;
            tokenRow.RefreshToken = refreshToken;

            await _unitOfWork.CompleteAsync();

            return authenticationTokenResponse;
        }
    }
}
