using MediatR;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.CreatorSkills.Queries.GetCreatorSkills
{
    public class GetCreatorSkillsQueryHandler :
        IRequestHandler<GetCreatorSkillsQuery, Result<PageSourcePagination<GetCreatorSkillsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCreatorSkillsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PageSourcePagination<GetCreatorSkillsResponse>>> Handle(GetCreatorSkillsQuery command, CancellationToken cancellationToken)
        {
            var isExisting = await _unitOfWork.Creators.IsExistAsync(command.creatorId);
            if (!isExisting)
                return Result<PageSourcePagination<GetCreatorSkillsResponse>>.Failure("Creator Not Found", StatusCodes.Status404NotFound);

            var result = await _unitOfWork.CreatorSkills.GetAllPaginatedEnhancedAsync(
                filter: cs => cs.CreatorId == command.creatorId,
                selector: c => new GetCreatorSkillsResponse
                {
                    SkillId = c.SkillId,
                    SkillName = c.Skill.Name
                },
                pageNumber: command.pageNumber,
                pageSize: 20
                );

            return Result<PageSourcePagination<GetCreatorSkillsResponse>>.Success(result, "Paginated Creator Skills Retrieved Successfully");
        }
    }
}
