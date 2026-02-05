using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProSphere.Domain.Entities;
using ProSphere.ExternalServices.Interfaces.Authentication;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;
using ProSphere.Shared.DTOs.Authentication;

namespace ProSphere.Features.Authentication.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<AuthenticationTokenDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationTokenService _authenticationTokenService;

        public RefreshTokenCommandHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IAuthenticationTokenService authenticationTokenService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _authenticationTokenService = authenticationTokenService;
        }

        public async Task<Result<AuthenticationTokenDto>> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var tokenRow = await _unitOfWork.RefreshTokenAuths
                .GetAllAsIQueryable()
                .FirstOrDefaultAsync(rf => rf.RefreshToken == command.request.RefreshToken);

            if (tokenRow == null)
                return Result<AuthenticationTokenDto>.Failure("Token Not Found", StatusCodes.Status404NotFound);

            if (tokenRow.RefreshTokenExpiration <= DateTime.UtcNow)
                return Result<AuthenticationTokenDto>.Failure("Token Is Invalid Or Expired", StatusCodes.Status401Unauthorized);

            var user = await _userManager.FindByIdAsync(tokenRow.UserId);

            if (user == null)
                return Result<AuthenticationTokenDto>.Failure("User Not Found", StatusCodes.Status404NotFound);

            var authenticationTokenResponse = await _authenticationTokenService.GenerateAuthenticationTokens(user);

            return Result<AuthenticationTokenDto>.Success(authenticationTokenResponse, "Token Refreshed Successfully");
        }
    }
}
