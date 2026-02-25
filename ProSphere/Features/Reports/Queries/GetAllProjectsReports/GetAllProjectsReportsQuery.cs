using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Reports.Queries.GetAllProjectsReports
{
    public record GetAllProjectsReportsQuery(int pageNumber) : IRequest<Result<PageSourcePagination<GetAllProjectsReportsResponse>>>
    {
    }
}
