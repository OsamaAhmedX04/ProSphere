using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProSphere.Domain.Entities;
using ProSphere.Extensions;
using ProSphere.ResultResponse;

namespace ProSphere.Features.PasswordManagement.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidator<ChangePasswordRequest> _validator;

        public ChangePasswordCommandHandler(UserManager<ApplicationUser> userManager, IValidator<ChangePasswordRequest> validator)
        {
            _userManager = userManager;
            _validator = validator;
        }

        public async Task<Result> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.request.UserId);

            if (user == null)
                return Result.Failure("User Not Found", StatusCodes.Status404NotFound);

            var validationResult = await _validator.ValidateAsync(command.request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }

            var changePasswordResult = await _userManager
                .ChangePasswordAsync(user, command.request.CurrentPassword, command.request.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                var errors = changePasswordResult.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }

            return Result.Success("Password Has Changed Successfully");
        }
    }
}
