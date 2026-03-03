using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Notification.Commands.MarkNotificationAsRead
{
    public record MarkNotificationAsReadCommand(string userId, Guid notificationId) : IRequest<Result>
    {
    }
}
