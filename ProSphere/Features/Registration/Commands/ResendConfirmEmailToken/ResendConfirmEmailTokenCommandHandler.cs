using MediatR;
using Microsoft.AspNetCore.Identity;
using ProSphere.Domain.Entities;
using ProSphere.ExternalServices.Interfaces.Email;
using ProSphere.ResultResponse;
using LinkGenerator = ProSphere.Helpers.Generators.LinkGenerator;

namespace ProSphere.Features.Registration.Commands.ResendConfirmEmailToken
{
    public class ResendConfirmEmailTokenCommandHandler : IRequestHandler<ResendConfirmEmailTokenCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSenderService _emailSenderService;

        public ResendConfirmEmailTokenCommandHandler(UserManager<ApplicationUser> userManager, IEmailSenderService emailSenderService)
        {
            _userManager = userManager;
            _emailSenderService = emailSenderService;
        }

        public async Task<Result> Handle(ResendConfirmEmailTokenCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(command.email);

            if (user == null)
                return Result.Failure("User Not Found", StatusCodes.Status404NotFound);

            if (user.EmailConfirmed)
                return Result.Failure("This Email Is Already Confirmed", StatusCodes.Status409Conflict);

            var userRoles = await _userManager.GetRolesAsync(user);
            var role = userRoles.FirstOrDefault();

            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var confirmationLink = LinkGenerator.GenerateEmailConfirmationLink(user.Id, emailConfirmationToken);

            _emailSenderService.SendEmailConfirmationMail(
                command.email, confirmationLink,
                user.FirstName, user.LastName,
                role!);

            return Result.Success("We Have Sent Confirmation Link, Check Your Mail To Confirm Your Email");
        }
    }
}
