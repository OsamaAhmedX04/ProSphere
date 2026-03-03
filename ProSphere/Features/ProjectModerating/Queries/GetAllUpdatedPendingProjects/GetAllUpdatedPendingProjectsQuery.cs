using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectModerating.Queries.GetAllUpdatedPendingProjects
{
    public record GetAllUpdatedPendingProjectsQuery(int pageNumber)
        : IRequest<Result<PageSourcePagination<GetAllUpdatedPendingProjectsResponse>>>
    {
    }
}
