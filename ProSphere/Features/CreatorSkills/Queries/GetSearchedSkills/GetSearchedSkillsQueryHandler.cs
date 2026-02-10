using MediatR;
using ProSphere.Domain.Entities;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;
using System.Linq.Expressions;

namespace ProSphere.Features.CreatorSkills.Queries.GetSearchedSkills
{
    public class GetSearchedSkillsQueryHandler
        : IRequestHandler<GetSearchedSkillsQuery, Result<PageSourcePagination<GetSearchedSkillsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSearchedSkillsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PageSourcePagination<GetSearchedSkillsResponse>>>
            Handle(GetSearchedSkillsQuery query, CancellationToken cancellationToken)
        {
            Expression<Func<Skill, bool>> filter = s => true;

            if(!string.IsNullOrEmpty(query.searchTerm))
            {
                filter = s => s.Name.Contains(query.searchTerm);
            }

            var result = await _unitOfWork.Skills.GetAllPaginatedEnhancedAsync(
                filter: filter,
                selector: s => new GetSearchedSkillsResponse
                {
                    SkillId = s.Id,
                    SkillName = s.Name
                },
                pageNumber: query.pageNumber,
                pageSize: 10
                );

            return Result<PageSourcePagination<GetSearchedSkillsResponse>>.Success(result, "Paginated Skills Returned Successfully");
        }
    }
}
