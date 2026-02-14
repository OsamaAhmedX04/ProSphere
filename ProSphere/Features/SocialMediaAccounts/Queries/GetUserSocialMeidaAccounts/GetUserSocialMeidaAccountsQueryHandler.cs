using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProSphere.Domain.Entities;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.SocialMediaAccounts.Queries.GetUserSocialMeidaAccounts
{
    public class GetUserSocialMeidaAccountsQueryHandler
        : IRequestHandler<GetUserSocialMeidaAccountsQuery, Result<GetUserSocialMeidaAccountsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUserSocialMeidaAccountsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetUserSocialMeidaAccountsResponse>> 
            Handle(GetUserSocialMeidaAccountsQuery query, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == query.userId);
            if (user == null)
                return Result<GetUserSocialMeidaAccountsResponse>.Failure("User Not Found", 404);

            var result = await _unitOfWork.UsersSocialMedia.GetEnhancedAsync(
                filter: usm => usm.UserId == query.userId,
                selector: usm => new GetUserSocialMeidaAccountsResponse
                {
                    LinkedInURL = usm.LinkedInURL,
                    FacebookURL = usm.FacebookURL,
                    GitHubURL = usm.GitHubURL,
                    FirstPlatformName = usm.FirstPlatformName,
                    FirstPlatformURL = usm.FirstPlatformURL,
                    SecondPlatformName = usm.SecondPlatformName,
                    SecondPlatformURL = usm.SecondPlatformURL,
                    ThirdPlatformName = usm.ThirdPlatformName,
                    ThirdPlatformURL = usm.ThirdPlatformURL
                }
            ) ?? new GetUserSocialMeidaAccountsResponse();

            return Result<GetUserSocialMeidaAccountsResponse>.Success(result, "User's Social Media Retreived Successfully");
        }
    }
}
