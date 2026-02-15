using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Account.Queries.GetInvestorAccounts
{
    public record GetInvestorAccountsQuery(int pageNumber, string? userName = null) : IRequest<Result<PageSourcePagination<GetInvestorAccountsResponse>>>
    {
    }
}
