using MediatR;
using ProSphere.Domain.Enums;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Reports.Queries.GetAllUsersReports
{
    public class GetAllUsersReportsQueryHandler
        : IRequestHandler<GetAllUsersReportsQuery, Result<PageSourcePagination<GetAllUsersReportsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUsersReportsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PageSourcePagination<GetAllUsersReportsResponse>>>
            Handle(GetAllUsersReportsQuery query, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.ReportedUsers.GetAllPaginatedEnhancedAsync(
                filter: r => r.Status == Status.Pending,
                selector: r => new GetAllUsersReportsResponse
                {
                    CreatedAt = r.CreatedAt,
                    Description = r.Description,
                    Id = r.Id,
                    Reason = r.Reason.ToString(),
                    TargetUserId = r.UserId,
                    TargetUserName = r.User!.UserName!,
                    TargetUserEmail = r.User.Email!,
                    TargetUserFullName = r.User.FirstName + " " + r.User.LastName
                },
                pageNumber: query.pageNumber,
                pageSize: 30
                );

            return Result<PageSourcePagination<GetAllUsersReportsResponse>>.Success(result, "Paginated Users Reports Retrieved Successfully");
        }
    }
}
