using Microsoft.AspNetCore.SignalR;
using ProSphere.Domain.Enums;
using ProSphere.Hubs.Notification;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.Shared.DTOs.Notification;

namespace ProSphere.Services.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<NotificationHub> _hub;

        public NotificationService(IUnitOfWork unitOfWork, IHubContext<NotificationHub> hub)
        {
            _unitOfWork = unitOfWork;
            _hub = hub;
        }

        public async Task SendNotification(string receiverId, NotificationDto notification)
        {
            await _hub.Clients.Client(receiverId).SendAsync("ReceiveNotification", notification);

            var newNotification = new Domain.Entities.Notification
            {
                Id = notification.Id,
                Description = notification.Description,
                IsSeen = notification.IsSeen,
                SeenAt = notification.SeenAt,
                SentAt = DateTime.UtcNow,
                Status = Enum.Parse<NotificationStatus>(notification.Status, true),
                Type = Enum.Parse<NotificationType>(notification.Type, true),
                Title = notification.Title,
                UserId = receiverId,
            };

            await _unitOfWork.Notifications.AddAsync(newNotification);
        }
    }
}
