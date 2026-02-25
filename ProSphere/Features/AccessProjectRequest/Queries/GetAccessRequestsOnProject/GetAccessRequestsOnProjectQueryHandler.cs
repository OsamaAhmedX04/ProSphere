using LinqKit;
using MediatR;
using ProSphere.Domain.Constants.FileConstants;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;
using System.Linq.Expressions;

namespace ProSphere.Features.AccessProjectRequest.Queries.GetAccessRequestsOnProject
{
    public class GetAccessRequestsOnProjectQueryHandler
        : IRequestHandler<GetAccessRequestsOnProjectQuery, Result<PageSourcePagination<GetAccessRequestsOnProjectResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAccessRequestsOnProjectQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PageSourcePagination<GetAccessRequestsOnProjectResponse>>>
            Handle(GetAccessRequestsOnProjectQuery query, CancellationToken cancellationToken)
        {
            var isCreatorExist = await _unitOfWork.Creators.IsExistAsync(query.creatorId);
            if (!isCreatorExist)
                return Result<PageSourcePagination<GetAccessRequestsOnProjectResponse>>
                    .Failure("Creator Not Found", StatusCodes.Status404NotFound);

            var isProjectExist = await _unitOfWork.Projects.IsExistAsync(query.projectId);
            if (!isProjectExist)
                return Result<PageSourcePagination<GetAccessRequestsOnProjectResponse>>
                    .Failure("Project Not Found", StatusCodes.Status404NotFound);

            Expression<Func<ProjectAccessRequest, bool>> filter = p => p.ProjectId == query.projectId;
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
                selector: p => new GetAccessRequestsOnProjectResponse
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

            return Result<PageSourcePagination<GetAccessRequestsOnProjectResponse>>
                .Success(result, "Paginated Requests Retrieved Successfully");
        }
    }
}
