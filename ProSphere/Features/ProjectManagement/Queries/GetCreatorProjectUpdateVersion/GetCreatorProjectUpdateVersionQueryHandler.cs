using MediatR;
using ProSphere.Domain.Constants.FileConstants;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectManagement.Queries.GetCreatorProjectUpdateVersion
{
    public class GetCreatorProjectUpdateVersionQueryHandler
        : IRequestHandler<GetCreatorProjectUpdateVersionQuery, Result<GetCreatorProjectUpdateVersionResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCreatorProjectUpdateVersionQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetCreatorProjectUpdateVersionResponse>>
            Handle(GetCreatorProjectUpdateVersionQuery query, CancellationToken cancellationToken)
        {
            var isCreatorExist = await _unitOfWork.Creators.IsExistAsync(query.creatorId);
            if (!isCreatorExist)
                return Result<GetCreatorProjectUpdateVersionResponse>.Failure("Creator Not Found", StatusCodes.Status404NotFound);

            var updatedProject = await _unitOfWork.ProjectUpdatesHistories.FirstOrDefaultAsync(p => p.ProjectId == query.projectId);
            if (updatedProject is null)
                return Result<GetCreatorProjectUpdateVersionResponse>.Failure("No Updates For This Project", StatusCodes.Status404NotFound);

            var result = await _unitOfWork.ProjectUpdatesHistories.GetEnhancedAsync(
                filter: p => p.ProjectId == query.projectId,
                selector: p => new GetCreatorProjectUpdateVersionResponse
                {
                    ProjectId = query.projectId,
                    BusinessModel = p.BusinessModel,
                    EquityPercentage = p.EquityPercentage,
                    ExecutionPlan = p.ExecutionPlan,
                    FinancialDetails = p.FinancialDetails,
                    Market = p.Market,
                    ImagesURL = p.ImagesHistory.Select(x => SupabaseConstants.PrefixSupaURL + x.ImageUrl).ToList(),
                    MarketingStrategy = p.MarketingStrategy,
                    NeededInvestment = p.NeededInvestment,
                    Notes = p.Notes,
                    Problem = p.Problem,
                    ShortDescription = p.ShortDescription,
                    SolutionSummary = p.SolutionSummary,
                    Title = p.Title,
                    Status = p.Status.ToString(),
                    RejectionReason = p.RejectionReason
                });

            return Result<GetCreatorProjectUpdateVersionResponse>.Success(result!, "Updated Project Retrieved Successfully");
        }
    }
}
