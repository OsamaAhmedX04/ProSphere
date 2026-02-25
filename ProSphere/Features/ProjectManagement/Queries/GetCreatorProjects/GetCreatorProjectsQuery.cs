using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectManagement.Queries.GetCreatorProjects
{
    public record GetCreatorProjectsQuery(string creatorId, int pageNumber, string? title = null, string? status = null) : IRequest<Result<PageSourcePagination<GetCreatorProjectsResponse>>>
    {
    }
}
