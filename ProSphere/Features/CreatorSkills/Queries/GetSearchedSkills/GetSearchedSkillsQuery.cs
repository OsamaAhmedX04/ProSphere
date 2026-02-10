using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.CreatorSkills.Queries.GetSearchedSkills
{
    public record GetSearchedSkillsQuery(int pageNumber, string? searchTerm = null) : IRequest<Result<PageSourcePagination<GetSearchedSkillsResponse>>>
    {
    }
}
