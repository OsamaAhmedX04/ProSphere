using ProSphere.Shared.DTOs.Notification;

namespace ProSphere.Services.Notification
{
    public interface INotificationService
    {
        Task SendNotification(string receiverId, NotificationDto notification);
    }
}
