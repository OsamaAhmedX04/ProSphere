using MediatR;
using ProSphere.Domain.Constants.FileConstants;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProSphere.Features.ProjectVoting.Queries.GetAllVotersOnProject
{
    public class GetAllVotersOnProjectQueryHandler
        : IRequestHandler<GetAllVotersOnProjectQuery, Result<PageSourcePagination<GetAllVotersOnProjectResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllVotersOnProjectQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PageSourcePagination<GetAllVotersOnProjectResponse>>> 
            Handle(GetAllVotersOnProjectQuery query, CancellationToken cancellationToken)
        {
            var isProjectExist = await _unitOfWork.Projects.IsExistAsync(query.projectId);
            if (!isProjectExist) return Result<PageSourcePagination<GetAllVotersOnProjectResponse>>
                    .Failure("Project Not Found", StatusCodes.Status404NotFound);

            var result = await _unitOfWork.ProjectsVotes.GetAllPaginatedEnhancedAsync(
                filter: v => v.ProjectId == query.projectId,
                selector: v => new GetAllVotersOnProjectResponse
                {
                    CreatorId = v.CreatorId,
                    UserProfileImageURL = v.Creator.ImageProfileURL == null ?
                                                            null : SupabaseConstants.PrefixSupaURL + v.Creator.ImageProfileURL,
                    CreatorName = v.Creator.FullName,
                    UserName = v.Creator.User.UserName!,
                    HeadLine = v.Creator.HeadLine
                },
                pageNumber: query.pageNumber,
                pageSize: 20
                );

            return Result<PageSourcePagination<GetAllVotersOnProjectResponse>>
                .Success(result, "Paginated Voters Retrieved Successfully");
        }
    }
}
