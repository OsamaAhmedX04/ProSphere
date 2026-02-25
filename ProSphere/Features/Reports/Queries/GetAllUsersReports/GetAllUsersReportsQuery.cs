using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Reports.Queries.GetAllUsersReports
{
    public record GetAllUsersReportsQuery(int pageNumber) : IRequest<Result<PageSourcePagination<GetAllUsersReportsResponse>>>
    {
    }
}
