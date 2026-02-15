using MediatR;
using Microsoft.AspNetCore.Identity;
using ProSphere.Domain.Entities;
using ProSphere.ExternalServices.Interfaces.Email;
using ProSphere.ResultResponse;
using LinkGenerator = ProSphere.Helpers.Generators.LinkGenerator;

namespace ProSphere.Features.PasswordRecovery.Commands.ResendResetPasswordToken
{
    public class ResendResetPasswordTokenCommandHandler : IRequestHandler<ResendResetPasswordTokenCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSenderService _emailSenderService;

        public ResendResetPasswordTokenCommandHandler(UserManager<ApplicationUser> userManager, IEmailSenderService emailSenderService)
        {
            _userManager = userManager;
            _emailSenderService = emailSenderService;
        }

        public async Task<Result> Handle(ResendResetPasswordTokenCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(command.email);

            if (user == null)
                return Result.Failure("User Not Found", StatusCodes.Status404NotFound);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetPasswordLink = LinkGenerator.GenerateResetPasswordLink(user.Id, token);

            _emailSenderService.SendResetPasswordMail(command.email, resetPasswordLink);

            return Result.Success("Resetting Password Mail has been Sent To Your Email");
        }
    }
}
