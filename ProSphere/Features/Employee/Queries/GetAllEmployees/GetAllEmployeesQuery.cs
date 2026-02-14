using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Employee.Queries.GetAllEmployees
{
    public record GetAllEmployeesQuery(int pageNumber, string? name = null, string? country = null) : IRequest<Result<PageSourcePagination<GetAllEmployeesResponse>>>
    {
    }
}
