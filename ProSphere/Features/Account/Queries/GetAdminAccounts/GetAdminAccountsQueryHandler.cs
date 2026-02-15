using MediatR;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Account.Queries.GetAdminAccounts
{
    public class GetAdminAccountsQueryHandler : IRequestHandler<GetAdminAccountsQuery, Result<PageSourcePagination<GetAdminAccountsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAdminAccountsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PageSourcePagination<GetAdminAccountsResponse>>>
            Handle(GetAdminAccountsQuery query, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Admins.GetAllPaginatedEnhancedAsync(
                filter: a => true,
                selector: admin => new GetAdminAccountsResponse
                {
                    FirstName = admin.User.FirstName,
                    LastName = admin.User.LastName,
                    Gender = admin.User.Gender.ToString(),
                    IsSuperAdmin = admin.IsSuperAdmin
                },
                orderBy: q => q.OrderByDescending(a => a.IsSuperAdmin),
                pageNumber: query.pageNumber,
                pageSize: 20
                );

            return Result<PageSourcePagination<GetAdminAccountsResponse>>.Success(result, "Paginated Admin Accounts Retreived Successfully");
        }
    }
}
