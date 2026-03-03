using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Notification.Commands.DeleteNotification
{
    public record DeleteNotificationCommand(string userId, Guid notificationId) : IRequest<Result>
    {
    }
}
