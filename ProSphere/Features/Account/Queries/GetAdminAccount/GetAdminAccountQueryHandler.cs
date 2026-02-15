using MediatR;
using Microsoft.Extensions.Caching.Memory;
using ProSphere.Domain.Constants.CacheConstants;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Account.Queries.GetAdminAccount
{
    public class GetAdminAccountQueryHandler : IRequestHandler<GetAdminAccountQuery, Result<GetAdminAccountResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public GetAdminAccountQueryHandler(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<Result<GetAdminAccountResponse>> Handle(GetAdminAccountQuery query, CancellationToken cancellationToken)
        {
            if (_cache.TryGetValue(CacheKey.GetAdminAccountKey(query.userId), out GetAdminAccountResponse cachedResult))
                return Result<GetAdminAccountResponse>.Success(cachedResult, "Admin Account Retrieved Successfully");

            var result = await _unitOfWork.Admins.GetEnhancedAsync(
                filter: a => a.Id == query.userId,
                selector: a => new GetAdminAccountResponse
                {
                    FirstName = a.User.FirstName,
                    LastName = a.User.LastName,
                    Gender = a.User.Gender.ToString(),
                    IsSuperAdmin = a.IsSuperAdmin,
                });

            if (result is null)
                return Result<GetAdminAccountResponse>.Failure("User Not Found", StatusCodes.Status404NotFound);

            var cacheEntryOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                SlidingExpiration = TimeSpan.FromMinutes(30),
            };

            return Result<GetAdminAccountResponse>.Success(result, "Admin Account Retrieved Successfully");
        }
    }
}
