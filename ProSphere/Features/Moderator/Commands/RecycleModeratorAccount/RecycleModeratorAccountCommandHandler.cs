using MediatR;
using Microsoft.AspNetCore.Identity;
using ProSphere.Domain.Constants;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.Helpers;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Moderator.Commands.RecycleModeratorAccount
{
    public class RecycleModeratorAccountCommandHandler : IRequestHandler<RecycleModeratorAccountCommand, Result<RecycleModeratorAccountResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RecycleModeratorAccountCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<RecycleModeratorAccountResponse>> Handle(RecycleModeratorAccountCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.moderatorId);

            if (user == null)
                return Result<RecycleModeratorAccountResponse>.Failure("User Not Found", StatusCodes.Status404NotFound);

            var userRoles = await _userManager.GetRolesAsync(user);
            if (!(userRoles.Contains(Role.Moderator) || userRoles.Contains(Role.InActiveModerator)))
                return Result<RecycleModeratorAccountResponse>.Failure("User Not Found", StatusCodes.Status404NotFound);

            var response = new RecycleModeratorAccountResponse { TempPassword = PasswordGenerator.Generate(PasswordDificulty.Low) };

            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, response.TempPassword);

            await _userManager.RemoveFromRoleAsync(user, Role.Moderator);
            await _userManager.AddToRoleAsync(user, Role.InActiveModerator);

            return Result<RecycleModeratorAccountResponse>.Success(response, "Moderator Recycled Successfully");
        }
    }
}
