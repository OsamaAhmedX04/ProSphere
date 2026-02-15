using MediatR;
using ProSphere.Features.Account.Queries.GetInvestorAccounts;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Search.Queries.SearchForInvestors
{
    public record SearchForInvestorsQuery
        (int pageNumber, string? userId = null, string? userName = null, bool? verified = null, bool? financial = null,
        bool? professional = null)
        : IRequest<Result<PageSourcePagination<GetInvestorAccountsResponse>>>
    {
    }
}
