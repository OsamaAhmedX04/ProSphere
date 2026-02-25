using MediatR;
using ProSphere.Domain.Enums;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Reports.Queries.GetAllProjectsReports
{
    public class GetAllProjectsReportsQueryHandler
        : IRequestHandler<GetAllProjectsReportsQuery, Result<PageSourcePagination<GetAllProjectsReportsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProjectsReportsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PageSourcePagination<GetAllProjectsReportsResponse>>>
            Handle(GetAllProjectsReportsQuery query, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.ReportedProjects.GetAllPaginatedEnhancedAsync(
                filter: r => r.Status == Status.Pending,
                selector: r => new GetAllProjectsReportsResponse
                {
                    CreatedAt = r.CreatedAt,
                    Description = r.Description,
                    Id = r.Id,
                    ProjectId = r.ProjectId,
                    ProjectTitle = r.Project.Title,
                    Reason = r.Reason.ToString()
                },
                pageNumber: query.pageNumber,
                pageSize: 30
                );

            return Result<PageSourcePagination<GetAllProjectsReportsResponse>>.Success(result, "Paginated Projects Reports Retrieved Successfully");
        }
    }
}
