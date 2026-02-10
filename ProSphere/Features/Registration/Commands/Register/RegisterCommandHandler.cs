using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProSphere.Data.Context;
using ProSphere.Domain.Constants;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.Extensions;
using ProSphere.ExternalServices.Interfaces.Email;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;
using LinkGenerator = ProSphere.Helpers.LinkGenerator;

namespace ProSphere.Features.Registration.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidator<RegisterRequest> _validator;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _db;

        public RegisterCommandHandler(
            UserManager<ApplicationUser> userManager, IValidator<RegisterRequest> validator,
            IEmailSenderService emailSenderService, IUnitOfWork unitOfWork, AppDbContext db)
        {
            _userManager = userManager;
            _validator = validator;
            _emailSenderService = emailSenderService;
            _unitOfWork = unitOfWork;
            _db = db;
        }

        public async Task<Result> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if (await IsDeletedUser(command.request.Email))
                return Result.Failure("This Email Cannot Make Account Except After 30 Day From Being Deleted", StatusCodes.Status400BadRequest);

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

            if (command.request.Role == Role.Creator)
            {
                await _unitOfWork.Creators.AddAsync(new Creator
                {
                    Id = newUser.Id,
                    UserName = newUser.FirstName + " " + newUser.LastName,
                });
            }
            else
            {
                await _unitOfWork.Investors.AddAsync(new Investor
                {
                    Id = newUser.Id,
                    InvestorLevel = InvestorLevel.None,
                    UserName = newUser.FirstName + " " + newUser.LastName,
                });
            }
            await _unitOfWork.CompleteAsync();



            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

            var confirmationLink = LinkGenerator.GenerateEmailConfirmationLink(newUser.Id, emailConfirmationToken);

            _emailSenderService.SendEmailConfirmationMail(
                command.request.Email, confirmationLink,
                command.request.FirstName, command.request.LastName,
                command.request.Role);

            return Result.Success("User Registered Successfully , Check Your Mail To Confirm Your Email");
        }

        private async Task<bool> IsDeletedUser(string userEmail) =>
            await _unitOfWork.UserAccountHistories.FirstOrDefaultAsync(h => h.Email == userEmail) != null;

    }
}
