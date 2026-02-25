using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.AccessProjectRequest.Queries.GetAllCreatorAccessedProjectRequests
{
    public record GetAllCreatorAccessedProjectRequestsQuery(int pageNumber, string creatorId, string? status = null)
        : IRequest<Result<PageSourcePagination<GetAllCreatorAccessedProjectRequestsResponse>>>
    {
    }
}
