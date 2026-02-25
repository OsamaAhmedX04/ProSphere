using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.AccessProjectRequest.Queries.GetAccessRequestsOnProject
{
    public record GetAccessRequestsOnProjectQuery(int pageNumber, string creatorId, Guid projectId, string? status = null)
        : IRequest<Result<PageSourcePagination<GetAccessRequestsOnProjectResponse>>>
    {
    }
}
