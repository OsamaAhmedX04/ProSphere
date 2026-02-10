using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.CreatorSkills.Queries.GetSkillsStatus
{
    public record GetSkillsStatusQuery(int pageNumber) : IRequest<Result<PageSourcePagination<GetSkillsStatusResponse>>>
    {
    }
}
