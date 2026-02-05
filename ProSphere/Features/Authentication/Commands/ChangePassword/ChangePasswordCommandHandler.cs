using MediatR;
using Microsoft.AspNetCore.Identity;
using ProSphere.Domain.Entities;
using ProSphere.Extensions;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Authentication.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ChangePasswordCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.request.UserId);

            if (user == null)
                return Result.Failure("User Not Found", 404);

            var changePasswordResult = await _userManager
                .ChangePasswordAsync(user, command.request.CurrentPassword, command.request.NewPassword);

            if(!changePasswordResult.Succeeded)
            {
                var errors = changePasswordResult.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }

            return Result.Success("Password Has Changed Successfully");
        }
    }
}
