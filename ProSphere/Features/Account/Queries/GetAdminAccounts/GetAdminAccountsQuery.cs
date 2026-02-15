using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Account.Queries.GetAdminAccounts
{
    public record GetAdminAccountsQuery(int pageNumber) : IRequest<Result<PageSourcePagination<GetAdminAccountsResponse>>>
    {
    }
}
