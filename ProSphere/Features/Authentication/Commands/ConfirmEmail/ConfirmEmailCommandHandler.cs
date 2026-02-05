using MediatR;
using Microsoft.AspNetCore.Identity;
using ProSphere.Domain.Entities;
using ProSphere.Extensions;
using ProSphere.ExternalServices.Interfaces.Authentication;
using ProSphere.ResultResponse;
using ProSphere.Shared.DTOs.Authentication;
using System.Net;

namespace ProSphere.Features.Authentication.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Result<AuthenticationTokenDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthenticationTokenService _authenticationTokenService;

        public ConfirmEmailCommandHandler(UserManager<ApplicationUser> userManager, IAuthenticationTokenService authenticationTokenService)
        {
            _userManager = userManager;
            _authenticationTokenService = authenticationTokenService;
        }

        public async Task<Result<AuthenticationTokenDto>> Handle(ConfirmEmailCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.userId);

            if (user == null)
                return Result<AuthenticationTokenDto>.Failure("User Not Found", 404);

            var decodedToken = WebUtility.UrlDecode(command.token);

            var confirmingResult = await _userManager.ConfirmEmailAsync(user, decodedToken);

            if (!confirmingResult.Succeeded)
            {
                var errors = confirmingResult.ConvertErrorsToDictionary();
                return Result<AuthenticationTokenDto>.ValidationFailure(errors);
            }

            var authenticationTokenResponse = await _authenticationTokenService.GenerateAuthenticationTokens(user, false);

            return Result<AuthenticationTokenDto>.Success(authenticationTokenResponse, "Email Confirmed Successfully");
        }
    }
}
