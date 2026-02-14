using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProSphere.Domain.Constants;
using ProSphere.Domain.Entities;
using ProSphere.Extensions;
using ProSphere.ExternalServices.Interfaces.Authentication;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;
using ProSphere.Shared.DTOs.Authentication;

namespace ProSphere.Features.PasswordManagement.Commands.ChangeInActiveRolePassword
{
    public class ChangeInActiveRolePasswordCommandHandler
        : IRequestHandler<ChangeInActiveRolePasswordCommand, Result<AuthenticationTokenDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationTokenService _authenticationTokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidator<ChangeInActiveRolePasswordRequest> _validator;


        public ChangeInActiveRolePasswordCommandHandler(
            IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager,
            IAuthenticationTokenService authenticationTokenService, IValidator<ChangeInActiveRolePasswordRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _authenticationTokenService = authenticationTokenService;
            _validator = validator;
        }

        public async Task<Result<AuthenticationTokenDto>>
            Handle(ChangeInActiveRolePasswordCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.request.UserId);

            if (user == null)
                return Result<AuthenticationTokenDto>.Failure("User Not Found", StatusCodes.Status404NotFound);

            var validationResult = await _validator.ValidateAsync(command.request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.ConvertErrorsToDictionary();
                return Result<AuthenticationTokenDto>.ValidationFailure(errors);
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Contains(Role.Moderator))
            {
                var isavailableModerator = await _unitOfWork.Moderators.AnyAsync(m => m.IsUsed == false && m.Id == command.request.UserId);
                if (!isavailableModerator)
                    return Result<AuthenticationTokenDto>.Failure("Moderator Account Not Assigned Yet", StatusCodes.Status404NotFound);
            }

            var isSimilar = await _userManager.CheckPasswordAsync(user, command.request.NewPassword);
            if (isSimilar)
                return Result<AuthenticationTokenDto>.Failure("You Cannot Enter The Same Password", StatusCodes.Status400BadRequest);

            var changePasswordResult = await _userManager
            .ChangePasswordAsync(user, command.request.CurrentPassword, command.request.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                var errors = changePasswordResult.ConvertErrorsToDictionary();
                return Result<AuthenticationTokenDto>.ValidationFailure(errors);
            }


            if (userRoles.Contains(Role.Moderator))
            {
                await _userManager.RemoveFromRoleAsync(user, Role.InActiveModerator);
                await _userManager.AddToRoleAsync(user, Role.Moderator);
            }
            if (userRoles.Contains(Role.Admin))
            {
                var admin = await _unitOfWork.Admins.GetByIdAsync(user.Id);
                var isSuperAdmin = admin!.IsSuperAdmin;
                await _userManager.RemoveFromRoleAsync(user, Role.InActiveAdmin);

                if (isSuperAdmin)
                    await _userManager.AddToRoleAsync(user, Role.SuperAdmin);

                else
                    await _userManager.AddToRoleAsync(user, Role.Admin);
            }

            var authenticationTokenResponse = await _authenticationTokenService.GenerateAuthenticationTokens(user, command.request.RememberMe);

            return Result<AuthenticationTokenDto>.Success(authenticationTokenResponse, $"Password Has Been Changed Successfully And Role Is Activated");
        }
    }
}
