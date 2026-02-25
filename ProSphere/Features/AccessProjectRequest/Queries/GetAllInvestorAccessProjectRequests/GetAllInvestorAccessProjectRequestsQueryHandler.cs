using LinqKit;
using MediatR;
using ProSphere.Domain.Constants.FileConstants;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;
using System.Linq.Expressions;

namespace ProSphere.Features.AccessProjectRequest.Queries.GetAllInvestorAccessProjectRequests
{
    public class GetAllInvestorAccessProjectRequestsQueryHandler
        : IRequestHandler<GetAllInvestorAccessProjectRequestsQuery, Result<PageSourcePagination<GetAllInvestorAccessProjectRequestsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllInvestorAccessProjectRequestsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PageSourcePagination<GetAllInvestorAccessProjectRequestsResponse>>>
            Handle(GetAllInvestorAccessProjectRequestsQuery query, CancellationToken cancellationToken)
        {
            var isInvestorExist = await _unitOfWork.Investors.IsExistAsync(query.investorId);
            if (!isInvestorExist)
                return Result<PageSourcePagination<GetAllInvestorAccessProjectRequestsResponse>>
                    .Failure("Investor Not Found", StatusCodes.Status404NotFound);

            Expression<Func<ProjectAccessRequest, bool>> filter = p => p.InvestorId == query.investorId;
            Status? status;
            if (!string.IsNullOrEmpty(query.status))
            {
                status = query.status.ToLower() switch
                {
                    "pending" => Status.Pending,
                    "approved" => Status.Approved,
                    "rejected" => Status.Rejected,
                    _ => null
                };
                if (status.HasValue)
                    filter = filter.And(p => p.Status == status);
            }

            var result = await _unitOfWork.ProjectsAccessRequests.GetAllPaginatedEnhancedAsync(
            filter: filter,
            selector: p => new GetAllInvestorAccessProjectRequestsResponse
            {
                RequestId = p.Id,
                CreatorId = p.Project.CreatorId,
                ProjectId = p.ProjectId,
                ProjectTitle = p.Project.Title,
                CreatorFullName = p.Project.Creator.FullName,
                CreatorImageProfile =
                    p.Project.Creator.ImageProfileURL == null ? null : SupabaseConstants.PrefixSupaURL + p.Project.Creator.ImageProfileURL,
                Status = p.Status.ToString()
            },
            pageNumber: query.pageNumber,
            pageSize: 15
            );

            return Result<PageSourcePagination<GetAllInvestorAccessProjectRequestsResponse>>
                .Success(result, "Paginated Requests Retrieved Successfully");
        }
    }
}