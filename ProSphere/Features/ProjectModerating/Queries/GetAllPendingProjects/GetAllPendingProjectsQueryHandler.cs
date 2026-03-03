using MediatR;
using ProSphere.Domain.Constants.FileConstants;
using ProSphere.Domain.Enums;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectModerating.Queries.GetAllPendingProjects
{
    public class GetAllPendingProjectsQueryHandler
        : IRequestHandler<GetAllPendingProjectsQuery, Result<PageSourcePagination<GetAllPendingProjectsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllPendingProjectsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PageSourcePagination<GetAllPendingProjectsResponse>>>
            Handle(GetAllPendingProjectsQuery query, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Projects.GetAllPaginatedEnhancedAsync(
                filter: p => p.Status == Status.Pending && !p.UpdatedAt.HasValue,
                selector: p => new GetAllPendingProjectsResponse
                {
                    ProjectId = p.Id,
                    Title = p.Title,
                    ShortDescription = p.ShortDescription,
                    EquityPercentage = p.EquityPercentage,
                    Market = p.Market,
                    NeededInvestment = p.NeededInvestment,
                    Problem = p.Problem,
                    SolutionSummary = p.SolutionSummary,
                    ImagesURL = p.Images.Select(image => SupabaseConstants.PrefixSupaURL + image.ImageUrl).ToList(),
                    Status = p.Status.ToString(),
                },
                pageNumber: query.pageNumber,
                pageSize: 10
                );

            return Result<PageSourcePagination<GetAllPendingProjectsResponse>>
                .Success(result, "Paginated Creator's Pending Project Retrieved Successfully");
        }
    }
}
