using LinqKit;
using MediatR;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;
using System.Linq.Expressions;

namespace ProSphere.Features.Account.Queries.GetModeratorAccounts
{
    public class GetModeratorAccountsQueryHandler : IRequestHandler<GetModeratorAccountsQuery, Result<PageSourcePagination<GetModeratorAccountsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetModeratorAccountsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PageSourcePagination<GetModeratorAccountsResponse>>> Handle(GetModeratorAccountsQuery query, CancellationToken cancellationToken)
        {
            Expression<Func<Domain.Entities.Moderator, bool>> filter = m => true;

            if (!string.IsNullOrEmpty(query.userName))
                filter = filter.And(m => (m.User.FirstName + " " + m.User.LastName).Contains(query.userName));

            var moderators = await _unitOfWork.Moderators.GetAllPaginatedEnhancedAsync(
                filter: filter,
                selector: m => new GetModeratorAccountsResponse
                {
                    UserId = m.Id,
                    Email = m.User.Email!,
                    IsUsed = m.IsUsed,
                    Code = m.Code
                });

            return Result<PageSourcePagination<GetModeratorAccountsResponse>>
                .Success(moderators, "Retrieved Paginated Moderators Successfully");
        }
    }
}
