using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.CreatorSkills.Queries.GetCreatorSkills
{
    public record GetCreatorSkillsQuery(string creatorId, int pageNumber) :
        IRequest<Result<PageSourcePagination<GetCreatorSkillsResponse>>>
    {
    }
}
