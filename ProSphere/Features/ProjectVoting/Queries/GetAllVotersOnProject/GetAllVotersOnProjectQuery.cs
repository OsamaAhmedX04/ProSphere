using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectVoting.Queries.GetAllVotersOnProject
{
    public record GetAllVotersOnProjectQuery(int pageNumber, Guid projectId) : IRequest<Result<PageSourcePagination<GetAllVotersOnProjectResponse>>>
    {
    }
}
