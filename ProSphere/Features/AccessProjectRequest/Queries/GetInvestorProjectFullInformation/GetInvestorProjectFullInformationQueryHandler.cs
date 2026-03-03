using MediatR;
using ProSphere.Domain.Constants.FileConstants;
using ProSphere.Domain.Enums;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.AccessProjectRequest.Queries.GetInvestorProjectFullInformation
{
    public class GetInvestorProjectFullInformationQueryHandler
        : IRequestHandler<GetInvestorProjectFullInformationQuery, Result<GetInvestorProjectFullInformationResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetInvestorProjectFullInformationQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetInvestorProjectFullInformationResponse>>
            Handle(GetInvestorProjectFullInformationQuery query, CancellationToken cancellationToken)
        {
            var isInvestorHasAccess = await _unitOfWork.ProjectsAccessRequests
                .FirstOrDefaultAsync
                (r => r.InvestorId == query.investorId && r.ProjectId == query.projectId && r.Status == Status.Approved);

            if (isInvestorHasAccess == null)
                return Result<GetInvestorProjectFullInformationResponse>
                    .Failure("UnAuthorized Access", StatusCodes.Status401Unauthorized);

            var result = await _unitOfWork.Projects.GetEnhancedAsync(
                filter: p => p.Id == query.projectId,
                selector: p => new GetInvestorProjectFullInformationResponse
                {
                    ProjectId = p.Id,
                    CreatorId = p.CreatorId,
                    CreatorFullName = p.Creator.FullName,
                    CreatorImageProfile =
                            p.Creator.ImageProfileURL == null ? null : SupabaseConstants.PrefixSupaURL + p.Creator.ImageProfileURL,
                    HeadLine = p.Creator.HeadLine,
                    CreatorUserName = p.Creator.User.UserName!,
                    IsVerified = p.Creator.User.IsVerified,
                    Title = p.Title,
                    ShortDescription = p.ShortDescription,
                    Problem = p.Problem,
                    SolutionSummary = p.SolutionSummary,
                    Market = p.Market,
                    NeededInvestment = p.NeededInvestment,
                    EquityPercentage = p.EquityPercentage,
                    IsActive = p.IsActive,
                    IsInvested = p.IsInvested,
                    IsBlocked = p.IsBlocked,
                    IsBlockedDueToBannedUser = p.IsBlockedDueToBannedUser,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    ImagesURL = p.Images.Select(i => SupabaseConstants.PrefixSupaURL + i.ImageUrl).ToList(),
                    ExecutionPlan = p.Details.ExecutionPlan,
                    FinancialDetails = p.Details.FinancialDetails,
                    BusinessModel = p.Details.BusinessModel,
                    MarketingStrategy = p.Details.MarketingStrategy,
                    Notes = p.Details.Notes
                }
            );

            return Result<GetInvestorProjectFullInformationResponse>.Success(result!, "Project Details Retrieved Successfully");
        }
    }
}
