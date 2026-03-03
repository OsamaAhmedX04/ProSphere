using MediatR;
using ProSphere.Domain.Constants.FileConstants;
using ProSphere.Domain.Enums;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectModerating.Queries.GetAllUpdatedPendingProjects
{
    public class GetAllUpdatedPendingProjectsQueryHandler
        : IRequestHandler<GetAllUpdatedPendingProjectsQuery, Result<PageSourcePagination<GetAllUpdatedPendingProjectsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUpdatedPendingProjectsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PageSourcePagination<GetAllUpdatedPendingProjectsResponse>>>
            Handle(GetAllUpdatedPendingProjectsQuery query, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.ProjectUpdatesHistories.GetAllPaginatedEnhancedAsync(
                filter: p => p.Status == Status.Pending,
                selector: p => new GetAllUpdatedPendingProjectsResponse
                {
                    ProjectId = p.ProjectId,
                    Title = p.Title,
                    ShortDescription = p.ShortDescription,
                    EquityPercentage = p.EquityPercentage,
                    Market = p.Market,
                    NeededInvestment = p.NeededInvestment,
                    Problem = p.Problem,
                    SolutionSummary = p.SolutionSummary,
                    ImagesURL = p.ImagesHistory.Select(image => SupabaseConstants.PrefixSupaURL + image.ImageUrl).ToList(),
                    Status = p.Status.ToString(),
                },
                pageNumber: query.pageNumber,
                pageSize: 10
                );

            return Result<PageSourcePagination<GetAllUpdatedPendingProjectsResponse>>
                .Success(result, "Paginated Creator's Pending Project Retrieved Successfully");
        }
    }
}
