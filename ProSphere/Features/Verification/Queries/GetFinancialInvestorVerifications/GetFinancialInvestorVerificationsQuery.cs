using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Verification.Queries.GetFinancialInvestorVerifications
{
    public record GetFinancialInvestorVerificationsQuery(int pageNumber, string? status = null) : IRequest<Result<PageSourcePagination<GetFinancialInvestorVerificationsResponse>>>
    {
    }
}
