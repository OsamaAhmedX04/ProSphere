using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Notification.Commands.DeleteAllNotifications
{
    public record DeleteAllNotificationsCommand(string userId) : IRequest<Result>
    {
    }
}
