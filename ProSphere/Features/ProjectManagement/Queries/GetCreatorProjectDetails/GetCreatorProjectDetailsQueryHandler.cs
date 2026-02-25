using MediatR;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProSphere.Features.ProjectManagement.Queries.GetCreatorProjectDetails
{
    public class GetCreatorProjectDetailsQueryHandler 
        : IRequestHandler<GetCreatorProjectDetailsQuery, Result<GetCreatorProjectDetailsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCreatorProjectDetailsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetCreatorProjectDetailsResponse>> 
            Handle(GetCreatorProjectDetailsQuery query, CancellationToken cancellationToken)
        {
            var isCreatorExist = await _unitOfWork.Creators.IsExistAsync(query.creatorId);
            if (!isCreatorExist) return Result<GetCreatorProjectDetailsResponse>.Failure("Creator Not Found", StatusCodes.Status404NotFound);

            var isProjectExist = await _unitOfWork.Projects.IsExistAsync(query.projectId);
            if (!isProjectExist) return Result<GetCreatorProjectDetailsResponse>.Failure("Project Not Found", StatusCodes.Status404NotFound);

            var result = await _unitOfWork.ProjectsDetails.GetEnhancedAsync(
                filter: p => p.ProjectId == query.projectId,
                selector: p => new GetCreatorProjectDetailsResponse
                {
                    BusinessModel = p.BusinessModel,
                    ExecutionPlan = p.ExecutionPlan,
                    FinancialDetails = p.FinancialDetails,
                    MarketingStrategy = p.MarketingStrategy,
                    Notes = p.Notes,
                });

            return Result<GetCreatorProjectDetailsResponse>.Success(result!, "Project Details Retrieved Successfully");
        }
    }
}
