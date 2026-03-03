using MediatR;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Notification.Queries.GetAllNotifications
{
    public class GetAllNotificationsQueryHandler
        : IRequestHandler<GetAllNotificationsQuery, Result<PageSourcePagination<GetAllNotificationsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllNotificationsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PageSourcePagination<GetAllNotificationsResponse>>>
            Handle(GetAllNotificationsQuery query, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Notifications.GetAllPaginatedEnhancedAsync(
                filter: n => n.UserId == query.userId,
                selector: n => new GetAllNotificationsResponse
                {
                    Id = n.Id,
                    Description = n.Description,
                    IsSeen = n.IsSeen,
                    SeenAt = n.SeenAt,
                    Status = n.Status.ToString(),
                    SentAt = n.SentAt,
                    Title = n.Title,
                    Type = n.Type.ToString()
                },
                pageNumber: query.pageNumber,
                pageSize: 30
                );

            return Result<PageSourcePagination<GetAllNotificationsResponse>>.Success(result, "Paginated Notifications Retrieved Successfully");
        }
    }
}
