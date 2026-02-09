using LinqKit;
using MediatR;
using ProSphere.Domain.Entities;
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
            Expression<Func<Moderator, bool>> filter = m => true;

            if(!string.IsNullOrEmpty(query.userName))
                filter = filter.And(m => (m.User.FirstName + " " +  m.User.LastName).Contains(query.userName));

            var moderators = await _unitOfWork.Moderators.GetAllPaginatedEnhancedAsync(
                filter: filter,
                selector: m => new GetModeratorAccountsResponse
                {
                    UserId = m.Id,
                    Gender = m.User.Gender.ToString(),
                    Email = m.User.Email!
                });

            return Result<PageSourcePagination<GetModeratorAccountsResponse>>
                .Success(moderators, "Retrieved Paginated Moderators Successfully");
        }
    }
}
