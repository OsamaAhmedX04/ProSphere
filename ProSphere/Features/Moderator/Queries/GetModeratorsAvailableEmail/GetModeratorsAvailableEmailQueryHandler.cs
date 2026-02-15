using MediatR;
using Microsoft.Extensions.Caching.Memory;
using ProSphere.Domain.Constants.CacheConstants;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;
using System.Linq.Expressions;

namespace ProSphere.Features.Moderator.Queries.GetModeratorsAvailableEmail
{
    public class GetModeratorsAvailableEmailQueryHandler
        : IRequestHandler<GetModeratorsAvailableEmailQuery, Result<List<GetModeratorsAvailableEmailResponse>>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        public GetModeratorsAvailableEmailQueryHandler(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<Result<List<GetModeratorsAvailableEmailResponse>>>
            Handle(GetModeratorsAvailableEmailQuery query, CancellationToken cancellationToken)
        {
            if (_cache.TryGetValue(CacheKey.GetModeratorAvailableEmailsKey, out List<GetModeratorsAvailableEmailResponse> cachedResult))
                return Result<List<GetModeratorsAvailableEmailResponse>>.Success(cachedResult, "Moderators' Emails Retrieved Successfully");

            Expression<Func<Domain.Entities.Moderator, bool>> filter = m => true;


            var moderators = await _unitOfWork.Moderators.GetAllAsyncEnhanced(
                filter: m => !m.IsUsed,
                selector: m => new GetModeratorsAvailableEmailResponse
                {
                    Id = m.Id,
                    Email = m.User.Email!
                });

            var memoryCacheEntryOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2),
                SlidingExpiration = TimeSpan.FromMinutes(30)
            };

            _cache.Set(CacheKey.GetModeratorAvailableEmailsKey, moderators, memoryCacheEntryOptions);

            return Result<List<GetModeratorsAvailableEmailResponse>>
                .Success(moderators, "Moderators' Emails Retrieved Successfully");
        }
    }
}
