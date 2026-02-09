using MediatR;
using ProSphere.Features.Account.Queries.GetAdminAccount;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Account.Queries.GetModeratorAccounts
{
    public record GetModeratorAccountsQuery(int pageNumber, string? userName = null)
        : IRequest<Result<PageSourcePagination<GetModeratorAccountsResponse>>>
    {
    }
}
