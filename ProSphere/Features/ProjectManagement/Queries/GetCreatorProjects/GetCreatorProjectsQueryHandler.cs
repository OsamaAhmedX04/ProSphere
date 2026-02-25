using LinqKit;
using MediatR;
using ProSphere.Domain.Constants.FileConstants;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;
using System.Linq.Expressions;

namespace ProSphere.Features.ProjectManagement.Queries.GetCreatorProjects
{
    public class GetCreatorProjectsQueryHandler 
        : IRequestHandler<GetCreatorProjectsQuery, Result<PageSourcePagination<GetCreatorProjectsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCreatorProjectsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PageSourcePagination<GetCreatorProjectsResponse>>> 
            Handle(GetCreatorProjectsQuery query, CancellationToken cancellationToken)
        {
            var isCreatorExist = await _unitOfWork.Creators.IsExistAsync(query.creatorId);
            if (!isCreatorExist) return Result<PageSourcePagination<GetCreatorProjectsResponse>>
                    .Failure("Creator Not Found", StatusCodes.Status404NotFound);

            Expression<Func<Project, bool>> filter = p => p.CreatorId == query.creatorId;

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

            if (!string.IsNullOrEmpty(query.title))
                filter = filter.And(p => p.Title.Contains(query.title));


            var result = await _unitOfWork.Projects.GetAllPaginatedEnhancedAsync(
                filter: filter,
                selector: p => new GetCreatorProjectsResponse
                {
                    Title = p.Title,
                    ShortDescription = p.ShortDescription,
                    IsActive = p.IsActive,
                    IsInvested = p.IsInvested,
                    IsBlocked = p.IsBlocked,
                    ImagesURL = p.Images.Select(image => SupabaseConstants.PrefixSupaURL + image.ImageUrl).ToList(),
                    Status = p.Status.ToString(),
                },
                pageNumber: query.pageNumber,
                pageSize: 10
                );

            return Result<PageSourcePagination<GetCreatorProjectsResponse>>
                .Success(result, "Paginated Creator's Project Retrieved Successfully");
        }
    }
}
