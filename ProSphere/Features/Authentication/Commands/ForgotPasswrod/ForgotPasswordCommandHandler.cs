using MediatR;
using Microsoft.AspNetCore.Identity;
using ProSphere.Domain.Entities;
using ProSphere.ExternalServices.Interfaces.Email;
using ProSphere.ResultResponse;
using LinkGenerator = ProSphere.Helpers.LinkGenerator;

namespace ProSphere.Features.Authentication.Commands.ForgotPasswrod
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSenderService _emailSenderService;

        public ForgotPasswordCommandHandler(UserManager<ApplicationUser> userManager, IEmailSenderService emailSenderService)
        {
            _userManager = userManager;
            _emailSenderService = emailSenderService;
        }

        public async Task<Result> Handle(ForgotPasswordCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(command.email);

            if (user == null)
                return Result.Failure("User Not Found", StatusCodes.Status404NotFound);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetPasswordLink = LinkGenerator.GenerateResetPasswordLink(user.Id, token);

            _emailSenderService.SendResetPasswordMail(command.email, resetPasswordLink);

            return Result.Success("Resetting Password Mail Sent To Your Email");
        }
    }
}
