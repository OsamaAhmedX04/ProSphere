using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProSphere.Domain.Entities;
using ProSphere.Extensions;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.SocialMediaAccounts.Commands.SetSocialMediaAccounts
{
    public class SetSocialMediaAccountsCommandHandler : IRequestHandler<SetSocialMediaAccountsCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidator<SetSocialMediaAccountsRequest> _validator;

        public SetSocialMediaAccountsCommandHandler
            (IValidator<SetSocialMediaAccountsRequest> validator, UserManager<ApplicationUser> userManager)
        {
            _validator = validator;
            _userManager = userManager;
        }

        public async Task<Result> Handle(SetSocialMediaAccountsCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command.request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }

            var user = await _userManager.Users
                .Include(u => u.SocialMedia)
                .FirstOrDefaultAsync(u => u.Id == command.userId);

            if (user is null)
                return Result.Failure("User Not Found", StatusCodes.Status404NotFound);

            if (user.SocialMedia == null)
            {
                user.SocialMedia = new UserSocialMedia
                {
                    UserId = user.Id
                };
            }

            user.SocialMedia.FacebookURL = command.request.FacebookURL;
            user.SocialMedia.LinkedInURL = command.request.LinkedInURL;
            user.SocialMedia.GitHubURL = command.request.GitHubURL;
            user.SocialMedia.FirstPlatformName = command.request.FirstPlatformName;
            user.SocialMedia.FirstPlatformURL = command.request.FirstPlatformURL;
            user.SocialMedia.SecondPlatformName = command.request.SecondPlatformName;
            user.SocialMedia.SecondPlatformURL = command.request.SecondPlatformURL;
            user.SocialMedia.ThirdPlatformName = command.request.ThirdPlatformName;
            user.SocialMedia.ThirdPlatformURL = command.request.ThirdPlatformURL;

            await _userManager.UpdateAsync(user);

            return Result.Success("Social Media Accounts Updated Successfully");
        }
    }
}
