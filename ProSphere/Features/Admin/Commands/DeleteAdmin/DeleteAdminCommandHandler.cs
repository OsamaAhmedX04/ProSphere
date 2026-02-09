using MediatR;
using Microsoft.AspNetCore.Identity;
using ProSphere.Domain.Constants;
using ProSphere.Domain.Entities;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Admin.Commands.DeleteAdmin
{
    public class DeleteAdminCommandHandler : IRequestHandler<DeleteAdminCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteAdminCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> Handle(DeleteAdminCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.adminId);

            if (user == null)
                return Result.Failure("User Not Found", StatusCodes.Status404NotFound);

            var userRoles = await _userManager.GetRolesAsync(user);
            var role = userRoles.FirstOrDefault();

            if((role == Role.SuperAdmin) || (role != Role.Admin && role != Role.InActiveAdmin))
                return Result.Failure("User Not Found", StatusCodes.Status404NotFound);

            await _userManager.DeleteAsync(user);

            return Result.Success("Admin Account Has Been Deleted Successfully");
        }
    }
}
