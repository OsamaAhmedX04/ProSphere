using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Notification.Queries.GetAllNotifications
{
    public record GetAllNotificationsQuery(int pageNumber, string userId)
        : IRequest<Result<PageSourcePagination<GetAllNotificationsResponse>>>
    {
    }
}
