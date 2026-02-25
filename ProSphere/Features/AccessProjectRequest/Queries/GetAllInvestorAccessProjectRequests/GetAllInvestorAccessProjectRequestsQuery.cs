using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.AccessProjectRequest.Queries.GetAllInvestorAccessProjectRequests
{
    public record GetAllInvestorAccessProjectRequestsQuery(int pageNumber, string investorId, string? status = null)
        : IRequest<Result<PageSourcePagination<GetAllInvestorAccessProjectRequestsResponse>>>
    {
    }
}
