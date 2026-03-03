using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Search.Queries.SearchForProject
{
    public record SearchForProjectQuery(int pageNumber, string? userId = null, string? projectName = null)
        : IRequest<Result<PageSourcePagination<SearchForProjectResponse>>>
    {
    }
}
