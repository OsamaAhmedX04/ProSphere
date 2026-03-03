using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProSphere.Features.Notification.Commands.DeleteAllNotifications;
using ProSphere.Features.Notification.Commands.DeleteNotification;
using ProSphere.Features.Notification.Commands.MarkNotificationAsRead;
using ProSphere.Features.Notification.Queries.GetAllNotifications;

namespace ProSphere.Features.Notification.Endpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly ISender _sender;

        public NotificationController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllNotifications(int pageNumber, string userId)
        {
            var query = new GetAllNotificationsQuery(pageNumber, userId);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{notificationId}")]
        public async Task<IActionResult> MarkNotificationAsRead(string userId, Guid notificationId)
        {
            var command = new MarkNotificationAsReadCommand(userId, notificationId);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{userId}/{notificationId}")]
        public async Task<IActionResult> DeleteNotification(string userId, Guid notificationId)
        {
            var command = new DeleteNotificationCommand(userId, notificationId);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteAllNotifications(string userId)
        {
            var command = new DeleteAllNotificationsCommand(userId);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
}
