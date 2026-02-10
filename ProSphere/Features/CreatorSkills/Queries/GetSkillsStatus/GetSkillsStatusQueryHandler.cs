using MediatR;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.CreatorSkills.Queries.GetSkillsStatus
{
    public class GetSkillsStatusQueryHandler :
        IRequestHandler<GetSkillsStatusQuery, Result<PageSourcePagination<GetSkillsStatusResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSkillsStatusQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PageSourcePagination<GetSkillsStatusResponse>>> Handle(GetSkillsStatusQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Skills.GetAllPaginatedEnhancedAsync(
                filter: null,
                selector: s => new GetSkillsStatusResponse
                {
                    SkillName = s.Name,
                    NumberOfCreators = s.Creators.Count
                },
                orderBy: s => s.OrderByDescending(sk => sk.Creators.Count).ThenBy(sk => sk.Name),
                pageNumber: request.pageNumber,
                pageSize: 20
                );

            return Result<PageSourcePagination<GetSkillsStatusResponse>>.Success(result, "Paginated Skills Status Retrieved Successfully");
        }
    }
}
