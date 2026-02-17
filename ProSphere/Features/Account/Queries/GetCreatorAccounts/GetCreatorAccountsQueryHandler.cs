using LinqKit;
using MediatR;
using ProSphere.Domain.Constants.FileConstants;
using ProSphere.Domain.Entities;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;
using System.Linq.Expressions;

namespace ProSphere.Features.Account.Queries.GetCreatorAccounts
{
    public class GetCreatorAccountsQueryHandler
        : IRequestHandler<GetCreatorAccountsQuery, Result<PageSourcePagination<GetCreatorAccountsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCreatorAccountsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PageSourcePagination<GetCreatorAccountsResponse>>>
            Handle(GetCreatorAccountsQuery query, CancellationToken cancellationToken)
        {
            Expression<Func<Creator, bool>> filter = c => c.User.EmailConfirmed;

            if (!string.IsNullOrEmpty(query.userName))
            {
                filter = filter.And(c => c.FullName.Contains(query.userName));
            }

            var result = await _unitOfWork.Creators.GetAllPaginatedEnhancedAsync(
                filter: filter,
                selector: c => new GetCreatorAccountsResponse
                {
                    UserId = c.Id,
                    FullName = c.FullName,
                    UserName = c.User.UserName!,
                    HeadLine = c.HeadLine,
                    IsVerified = c.User.IsVerified,
                    ImageProfileURL = c.ImageProfileURL == null ? null : SupabaseConstants.PrefixSupaURL + c.ImageProfileURL,
                },
                pageNumber: query.pageNumber,
                pageSize: 20
                );

            return Result<PageSourcePagination<GetCreatorAccountsResponse>>.Success(result, "Paginated Creators Retreived Successfully");
        }
    }
}
