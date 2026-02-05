using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProSphere.Domain.Entities;
using ProSphere.Extensions;
using ProSphere.ResultResponse;
using System.Net;

namespace ProSphere.Features.Authentication.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidator<ResetPasswordRequest> _validator;

        public ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager, IValidator<ResetPasswordRequest> validator)
        {
            _userManager = userManager;
            _validator = validator;
        }

        public async Task<Result> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.userId);

            if (user == null)
                return Result.Failure("User Not Found", 404);

            var validationResult = await _validator.ValidateAsync(command.request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }

            var decodedToken = WebUtility.UrlDecode(command.token);

            var resettingResult = await _userManager.ResetPasswordAsync(user, decodedToken, command.request.NewPassword);

            if (!resettingResult.Succeeded)
            {
                var errors = resettingResult.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }

            return Result.Success("Password Has Resetted Successfully");
        }
    }
}
