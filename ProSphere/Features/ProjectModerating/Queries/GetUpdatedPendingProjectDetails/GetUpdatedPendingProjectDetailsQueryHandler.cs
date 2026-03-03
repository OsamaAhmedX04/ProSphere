using MediatR;
using ProSphere.Domain.Enums;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectModerating.Queries.GetUpdatedPendingProjectDetails
{
    public class GetUpdatedPendingProjectDetailsQueryHandler
         : IRequestHandler<GetUpdatedPendingProjectDetailsQuery, Result<GetUpdatedPendingProjectDetailsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUpdatedPendingProjectDetailsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetUpdatedPendingProjectDetailsResponse>>
            Handle(GetUpdatedPendingProjectDetailsQuery query, CancellationToken cancellationToken)
        {
            var isProjectExist = await _unitOfWork.Projects.IsExistAsync(query.projectId);
            if (!isProjectExist) return Result<GetUpdatedPendingProjectDetailsResponse>.Failure("Project Not Found", StatusCodes.Status404NotFound);

            var result = await _unitOfWork.ProjectUpdatesHistories.GetEnhancedAsync(
                filter: p => p.ProjectId == query.projectId && p.Status == Status.Pending,
                selector: p => new GetUpdatedPendingProjectDetailsResponse
                {
                    BusinessModel = p.BusinessModel,
                    ExecutionPlan = p.ExecutionPlan,
                    FinancialDetails = p.FinancialDetails,
                    MarketingStrategy = p.MarketingStrategy,
                    Notes = p.Notes,
                });

            return Result<GetUpdatedPendingProjectDetailsResponse>.Success(result!, "Pending Project Details Retrieved Successfully");
        }
    }
}
