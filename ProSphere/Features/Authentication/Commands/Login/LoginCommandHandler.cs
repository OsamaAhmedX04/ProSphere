using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProSphere.Domain.Constants;
using ProSphere.Domain.Entities;
using ProSphere.Extensions;
using ProSphere.ExternalServices.Interfaces.Authentication;
using ProSphere.ResultResponse;
using ProSphere.Shared.DTOs.Authentication;

namespace ProSphere.Features.Authentication.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthenticationTokenDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidator<LoginRequest> _validator;
        private readonly IAuthenticationTokenService _authenticationTokenService;

        public LoginCommandHandler(UserManager<ApplicationUser> userManager, IValidator<LoginRequest> validator, IAuthenticationTokenService authenticationTokenService)
        {
            _userManager = userManager;
            _validator = validator;
            _authenticationTokenService = authenticationTokenService;
        }

        public async Task<Result<AuthenticationTokenDto>> Handle(
            LoginCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command.request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ConvertErrorsToDictionary();
                return Result<AuthenticationTokenDto>.ValidationFailure(errors);
            }

            var user = await _userManager.FindByEmailAsync(command.request.Email);

            if (user == null)
                return Result<AuthenticationTokenDto>.Failure("User Not Found", StatusCodes.Status404NotFound);

            if (!user.EmailConfirmed)
                return Result<AuthenticationTokenDto>.Failure("Email Not Confirmed Yet , Check Your Mail", StatusCodes.Status400BadRequest);

            var loginResult = await _userManager.CheckPasswordAsync(user, command.request.Password);

            if (!loginResult)
                return Result<AuthenticationTokenDto>.Failure("Wrong Email Or Password", StatusCodes.Status400BadRequest);

            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Contains(Role.InActiveModerator) || userRoles.Contains(Role.InActiveAdmin))
                return Result<AuthenticationTokenDto>.Failure("This Is InActive Role, Please Change Password", StatusCodes.Status403Forbidden);

            var authenticationTokenResponse = await _authenticationTokenService.GenerateAuthenticationTokens(user, command.request.RememberMe);

            return Result<AuthenticationTokenDto>.Success(authenticationTokenResponse, "User Logged In Successfully");
        }
    }
}
