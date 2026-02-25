using MediatR;
using ProSphere.Domain.Constants.FileConstants;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectManagement.Queries.GetCreatorProjectData
{
    public class GetCreatorProjectDataQueryHandler : IRequestHandler<GetCreatorProjectDataQuery, Result<GetCreatorProjectDataResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCreatorProjectDataQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetCreatorProjectDataResponse>>
            Handle(GetCreatorProjectDataQuery query, CancellationToken cancellationToken)
        {
            var isUpdatedProject = await _unitOfWork.ProjectUpdatesHistories.IsExistAsync(query.projectId);

            var result = await _unitOfWork.Projects.GetEnhancedAsync(
                filter: p => p.Id == query.projectId,
                selector: p => new GetCreatorProjectDataResponse
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
                    UpdatedAt = p.UpdatedAt,
                    IsUpdated = isUpdatedProject
                });

            return Result<GetCreatorProjectDataResponse>
                .Success(result!, "Creator's Project Data Retrieved Successfully");
        }
    }
}
