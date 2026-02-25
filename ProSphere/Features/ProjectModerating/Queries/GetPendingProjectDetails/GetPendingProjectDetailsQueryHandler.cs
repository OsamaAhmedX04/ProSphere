using MediatR;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectModerating.Queries.GetPendingProjectDetails
{
    public class GetPendingProjectDetailsQueryHandler
        : IRequestHandler<GetPendingProjectDetailsQuery, Result<GetPendingProjectDetailsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPendingProjectDetailsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetPendingProjectDetailsResponse>>
            Handle(GetPendingProjectDetailsQuery query, CancellationToken cancellationToken)
        {
            var isProjectExist = await _unitOfWork.Projects.IsExistAsync(query.projectId);
            if (!isProjectExist) return Result<GetPendingProjectDetailsResponse>.Failure("Project Not Found", StatusCodes.Status404NotFound);

            var result = await _unitOfWork.ProjectsDetails.GetEnhancedAsync(
                filter: p => p.ProjectId == query.projectId,
                selector: p => new GetPendingProjectDetailsResponse
                {
                    BusinessModel = p.BusinessModel,
                    ExecutionPlan = p.ExecutionPlan,
                    FinancialDetails = p.FinancialDetails,
                    MarketingStrategy = p.MarketingStrategy,
                    Notes = p.Notes,
                });

            return Result<GetPendingProjectDetailsResponse>.Success(result!, "Pending Project Details Retrieved Successfully");
        }
    }
}
