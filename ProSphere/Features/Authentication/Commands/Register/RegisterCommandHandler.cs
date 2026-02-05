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
using ProSphere.Helpers;
using LinkGenerator = ProSphere.Helpers.LinkGenerator;

namespace ProSphere.Features.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidator<RegisterRequest> _validator;
        private readonly IEmailSenderService _emailSenderService;

        public RegisterCommandHandler(UserManager<ApplicationUser> userManager, IValidator<RegisterRequest> validator, IEmailSenderService emailSenderService)
        {
            _userManager = userManager;
            _validator = validator;
            _emailSenderService = emailSenderService;
        }

        public async Task<Result> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command.request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ConvertErrorsToDictionary();
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

            var confirmationLink = LinkGenerator.GenerateEmailConfirmationLink(newUser.Id, emailConfirmationToken);

            _emailSenderService.SendEmailConfirmationMail(
                command.request.Email, confirmationLink,
                command.request.FirstName, command.request.LastName,
                command.request.Role);

            return Result.Success("User Registered Successfully , Check Your Mail To Confirm Your Email");
        }


        

        
    }
}
