using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProSphere.Domain.Constants.CacheConstants;
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
        private readonly IMemoryCache _cache;

        public GetUserSocialMeidaAccountsQueryHandler(IUnitOfWork unitOfWork, IMemoryCache cache, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
            _userManager = userManager;
        }

        public async Task<Result<GetUserSocialMeidaAccountsResponse>>
            Handle(GetUserSocialMeidaAccountsQuery query, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == query.userId);
            if (user == null)
                return Result<GetUserSocialMeidaAccountsResponse>.Failure("User Not Found", 404);


            if (_cache.TryGetValue(CacheKey.GetUserSocialMediaAccountsKey(user.Id), out GetUserSocialMeidaAccountsResponse cachedResult))
                return Result<GetUserSocialMeidaAccountsResponse>.Success(cachedResult, "User's Social Media Retreived Successfully");

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

            var cacheEntryOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(3),
                SlidingExpiration = TimeSpan.FromMinutes(30)
            };
            _cache.Set(CacheKey.GetUserSocialMediaAccountsKey(user.Id), result, cacheEntryOptions);

            return Result<GetUserSocialMeidaAccountsResponse>.Success(result, "User's Social Media Retreived Successfully");
        }
    }
}
