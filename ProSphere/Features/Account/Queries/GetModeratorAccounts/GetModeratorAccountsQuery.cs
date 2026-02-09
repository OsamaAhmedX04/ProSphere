using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Account.Queries.GetModeratorAccounts
{
    public record GetModeratorAccountsQuery(int pageNumber, string? userName = null)
        : IRequest<Result<PageSourcePagination<GetModeratorAccountsResponse>>>
    {
    }
}
