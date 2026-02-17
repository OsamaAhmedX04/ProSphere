using MediatR;
using Microsoft.Extensions.Caching.Memory;
using ProSphere.Domain.Constants.CacheConstants;
using ProSphere.Domain.Constants.FileConstants;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Account.Queries.GetCreatorAccount
{
    public class GetCreatorAccountQueryHandler : IRequestHandler<GetCreatorAccountQuery, Result<GetCreatorAccountResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public GetCreatorAccountQueryHandler(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<Result<GetCreatorAccountResponse>> Handle(GetCreatorAccountQuery query, CancellationToken cancellationToken)
        {
            if (_cache.TryGetValue(CacheKey.GetCreatorAccountKey(query.userName), out GetCreatorAccountResponse cachedResponse))
                return Result<GetCreatorAccountResponse>.Success(cachedResponse, "Creator Account Retrieved Successfully");

            var result = await _unitOfWork.Creators.GetEnhancedAsync(
                filter: c => c.User.UserName == query.userName && c.User.EmailConfirmed,
                selector: c => new GetCreatorAccountResponse
                {
                    Username = c.User.UserName!,
                    FirstName = c.User.FirstName,
                    LastName = c.User.LastName,
                    HeadLine = c.HeadLine,
                    BIO = c.BIO,
                    Gender = c.User.Gender.ToString(),
                    ImageProfileURL = c.ImageProfileURL == null ? null : SupabaseConstants.PrefixSupaURL + c.ImageProfileURL,
                    IsVerified = c.User.IsVerified
                });

            if (result is null)
                return Result<GetCreatorAccountResponse>.Failure("User Not Found", StatusCodes.Status404NotFound);

            var cacheEntryOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                SlidingExpiration = TimeSpan.FromMinutes(30),
            };
            _cache.Set(CacheKey.GetCreatorAccountKey(query.userName), result, cacheEntryOptions);

            return Result<GetCreatorAccountResponse>.Success(result, "Creator Account Retrieved Successfullt");
        }
    }
}
