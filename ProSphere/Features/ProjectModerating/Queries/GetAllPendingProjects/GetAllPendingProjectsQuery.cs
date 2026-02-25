using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectModerating.Queries.GetAllPendingProjects
{
    public record GetAllPendingProjectsQuery(int pageNumber) : IRequest<Result<PageSourcePagination<GetAllPendingProjectsResponse>>>
    {
    }
}
