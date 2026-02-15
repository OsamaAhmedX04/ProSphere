using MediatR;
using Microsoft.Extensions.Caching.Memory;
using ProSphere.Domain.Constants.CacheConstants;
using ProSphere.Domain.Constants.FileConstants;
using ProSphere.Domain.Enums;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Account.Queries.GetInvestorAccount
{
    public class GetInvestorAccountQueryHandler : IRequestHandler<GetInvestorAccountQuery, Result<GetInvestorAccountResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public GetInvestorAccountQueryHandler(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<Result<GetInvestorAccountResponse>> Handle(GetInvestorAccountQuery query, CancellationToken cancellationToken)
        {
            if (_cache.TryGetValue(CacheKey.GetInvestorAccountKey(query.userId), out GetInvestorAccountResponse cachedResult))
                return Result<GetInvestorAccountResponse>.Success(cachedResult, "Investor Account Retrieved Successfully (from cache)");
            
            var result = await _unitOfWork.Investors.GetEnhancedAsync(
                filter: i => i.Id == query.userId,
                selector: i => new GetInvestorAccountResponse
                {
                    FirstName = i.User.FirstName,
                    LastName = i.User.LastName,
                    Gender = i.User.Gender.ToString(),
                    ImageProfileURL = SupabaseConstants.PrefixSupaURL + i.ImageProfileURL ?? null,
                    HeadLine = i.HeadLine,
                    BIO = i.BIO,
                    IsVerified = i.User.IsVerified,
                    IsFinancail = i.InvestorLevel == InvestorLevel.Professional
                                  ||
                                  i.InvestorLevel == InvestorLevel.Financial,

                    IsProfessional = i.InvestorLevel == InvestorLevel.Professional
                });

            if (result is null)
                return Result<GetInvestorAccountResponse>.Failure("User Not Found", StatusCodes.Status404NotFound);

            var cacheEntryOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                SlidingExpiration = TimeSpan.FromMinutes(30),
            };
            _cache.Set(CacheKey.GetInvestorAccountKey(query.userId), result, cacheEntryOptions);

            return Result<GetInvestorAccountResponse>.Success(result, "Investor Account Retrieved Successfullt");
        }
    }
}
