using MediatR;
using ProSphere.Domain.Constants.FileConstants;
using ProSphere.Domain.Enums;
using ProSphere.Features.ProjectManagement.Queries.GetCreatorProjects;
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
                filter: p => p.Status == Status.Pending,
                selector: p => new GetAllPendingProjectsResponse
                {
                    Title = p.Title,
                    ShortDescription = p.ShortDescription,
                    CreatedAt = p.CreatedAt,
                    EquityPercentage = p.EquityPercentage,
                    IsActive = p.IsActive,
                    IsInvested = p.IsInvested,
                    Market = p.Market,
                    NeededInvestment = p.NeededInvestment,
                    Problem = p.Problem,
                    SolutionSummary = p.SolutionSummary,
                    ImagesURL = p.Images.Select(image => SupabaseConstants.PrefixSupaURL + image.ImageUrl).ToList(),
                    Status = p.Status.ToString(),
                    UpdatedAt = p.UpdatedAt
                },
                pageNumber: query.pageNumber,
                pageSize: 10
                );

            return Result<PageSourcePagination<GetAllPendingProjectsResponse>>
                .Success(result, "Paginated Creator's Pending Project Retrieved Successfully");
        }
    }
}
