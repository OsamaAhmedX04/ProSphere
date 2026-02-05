using FluentValidation;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProSphere.Domain.Constants;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.Extensions;
using ProSphere.ExternalServices.Interfaces.Email;
using ProSphere.ResultResponse;
using System.Net;

namespace ProSphere.Features.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidator<RegisterRequest> _validator;

        public RegisterCommandHandler(UserManager<ApplicationUser> userManager, IValidator<RegisterRequest> validator)
        {
            _userManager = userManager;
            _validator = validator;
        }

        public async Task<Result> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(command.request, cancellationToken);
            if (!result.IsValid)
            {
                var errors = result.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }

            var newUser = new ApplicationUser
            {
                UserName = command.request.Email,
                Email = command.request.Email,
                FirstName = command.request.FirstName,
                LastName = command.request.LastName,
                Gender = command.request.Gender.ToLower() == Gender.Male.ToString().ToLower() ? Gender.Male : Gender.Female
            };

            var createNewUserResult = await _userManager.CreateAsync(newUser, command.request.Password);

            if (!createNewUserResult.Succeeded)
            {
                var errors = createNewUserResult.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(newUser, command.request.Role);

            if (!addToRoleResult.Succeeded)
            {
                var errors = addToRoleResult.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }

            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

            var confirmationLink = GenerateConfirmationLink(newUser.Id, emailConfirmationToken);

            await SendEmailConfirmationMail(
                command.request.Email, confirmationLink,
                command.request.FirstName, command.request.LastName,
                command.request.Role);

            return Result.Success("User Registered Successfully , Check Your Mail To Confirm Your Email");
        }


        private async Task SendEmailConfirmationMail(
            string email, string confirmationLink, string firstName, string lastName, string role)
        {

            BackgroundJob.Enqueue<IEmailService>(
                service => service.SendEmailAsync(
                    email,
                    "Confirm Your Email",
                    EmailBody.GetEmailConfirmationBody(email, confirmationLink, firstName, lastName, role)
                    )
                );
        }

        private string GenerateConfirmationLink(string userId, string token)
        {
            var encodedToken = WebUtility.UrlEncode(token);

            var confirmationLink = $"https://app.prosphere.com/confirm-email?userId={userId}&token={encodedToken}";

            return confirmationLink;
        }
    }
}
