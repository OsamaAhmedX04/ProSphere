using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Account.Queries.GetCreatorAccounts
{
    public record GetCreatorAccountsQuery(int pageNumber, string? userName = null) : IRequest<Result<PageSourcePagination<GetCreatorAccountsResponse>>>
    {
    }
}
