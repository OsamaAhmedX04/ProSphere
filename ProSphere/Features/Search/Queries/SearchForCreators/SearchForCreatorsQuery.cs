using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Search.Queries.SearchForCreators
{
    public record SearchForCreatorsQuery
        (int pageNumber, string? userId = null, string? userName = null, bool? verified = null)
        : IRequest<Result<PageSourcePagination<SearchForCreatorsResponse>>>
    {
    }
}
