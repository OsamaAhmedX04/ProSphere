using MediatR;
using Microsoft.Extensions.Caching.Memory;
using ProSphere.Domain.Constants.CacheConstants;
using ProSphere.Features.Account.Queries.GetModeratorAccounts;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Account.Queries.GetModeratorAccount
{
    public class GetModeratorAccountQueryHandler : IRequestHandler<GetModeratorAccountQuery, Result<GetModeratorAccountResponse>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public GetModeratorAccountQueryHandler(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<Result<GetModeratorAccountResponse>> Handle(GetModeratorAccountQuery query, CancellationToken cancellationToken)
        {

            if(_cache.TryGetValue(CacheKey.GetModeratorAccountKey(query.userId), out GetModeratorAccountResponse cachedModeratorAccount))
            {
                return Result<GetModeratorAccountResponse>.Success(cachedModeratorAccount, "Retrieved Moderator Account from Cache Successfully");
            }

            var moderatorAccount = new GetModeratorAccountResponse();

            var moderator = await _unitOfWork.Moderators.GetEnhancedAsync(
                filter: m => m.Id == query.userId,
                selector: m => new
                {
                    UserId = m.Id,
                    Email = m.User.Email!,
                    IsUsed = m.IsUsed,
                    Code = m.Code,
                });

            if(moderator is null)
                return Result<GetModeratorAccountResponse>.Failure("Moderator Account Not Found", StatusCodes.Status404NotFound);

            moderatorAccount.UserId = moderator.UserId;
            moderatorAccount.Email = moderator.Email;
            moderatorAccount.IsUsed = moderator.IsUsed;
            moderatorAccount.Code = moderator.Code;

            var employee = await _unitOfWork.Employees.GetEnhancedAsync(
                filter: e => e.AssignedTo == query.userId,
                selector: e => new
                {
                    e.Id,
                    e.Name,
                    e.Email,
                    e.Phone,
                    e.Country
                });

            if (employee is not null)
            {
                moderatorAccount.EmployeeId = employee.Id;
                moderatorAccount.EmployeeName = employee.Name;
                moderatorAccount.EmployeeEmail = employee.Email;
                moderatorAccount.EmployeePhone = employee.Phone;
                moderatorAccount.EmployeeCountry = employee.Country;
            }

            var cacheEntryOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                SlidingExpiration = TimeSpan.FromMinutes(30)
            };

            _cache.Set(CacheKey.GetModeratorAccountKey(query.userId), moderatorAccount, cacheEntryOptions);

            return Result<GetModeratorAccountResponse>.Success(moderatorAccount, "Retrieved Moderator Account Successfully");
        }
    }
}
