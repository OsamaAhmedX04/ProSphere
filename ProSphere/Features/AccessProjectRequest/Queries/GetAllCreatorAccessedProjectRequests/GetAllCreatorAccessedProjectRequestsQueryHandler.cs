using LinqKit;
using MediatR;
using ProSphere.Domain.Constants.FileConstants;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;
using System.Linq.Expressions;

namespace ProSphere.Features.AccessProjectRequest.Queries.GetAllCreatorAccessedProjectRequests
{
    public class GetAllCreatorAccessedProjectRequestsQueryHandler
        : IRequestHandler<GetAllCreatorAccessedProjectRequestsQuery, Result<PageSourcePagination<GetAllCreatorAccessedProjectRequestsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCreatorAccessedProjectRequestsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PageSourcePagination<GetAllCreatorAccessedProjectRequestsResponse>>>
            Handle(GetAllCreatorAccessedProjectRequestsQuery query, CancellationToken cancellationToken)
        {
            var isCreatorExist = await _unitOfWork.Creators.IsExistAsync(query.creatorId);
            if (!isCreatorExist)
                return Result<PageSourcePagination<GetAllCreatorAccessedProjectRequestsResponse>>
                    .Failure("Creator Not Found", StatusCodes.Status404NotFound);

            Expression<Func<ProjectAccessRequest, bool>> filter = p => p.Project.CreatorId == query.creatorId;
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
                selector: p => new GetAllCreatorAccessedProjectRequestsResponse
                {
                    RequestId = p.Id,
                    InvestorId = p.InvestorId,
                    ProjectId = p.ProjectId,
                    ProjectTitle = p.Project.Title,
                    InvestorFullName = p.Investor.FullName,
                    InvestorImageProfileURL =
                        p.Investor.ImageProfileURL == null ? null : SupabaseConstants.PrefixSupaURL + p.Investor.ImageProfileURL,
                    Status = p.Status.ToString()
                },
                pageNumber: query.pageNumber,
                pageSize: 15
                );

            return Result<PageSourcePagination<GetAllCreatorAccessedProjectRequestsResponse>>
                .Success(result, "Paginated Requests Retrieved Successfully");
        }
    }
}
