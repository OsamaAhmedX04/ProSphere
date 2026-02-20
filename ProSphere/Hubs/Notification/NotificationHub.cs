namespace ProSphere.Hubs.Notification
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;

    [Authorize]
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            Console.WriteLine($"User connected: {userId}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.UserIdentifier;
            Console.WriteLine($"User disconnected: {userId}");
            await base.OnDisconnectedAsync(exception);
        }
    }
}


//using Microsoft.AspNetCore.SignalR;

//public class NotificationService
//{
//    private readonly IHubContext<NotificationHub> _hubContext;

//    public NotificationService(IHubContext<NotificationHub> hubContext)
//    {
//        _hubContext = hubContext;
//    }

//    // إرسال إشعار لمستخدم معين
//    public async Task SendNotification(string userId, NotificationDto notification)
//    {
//        await _hubContext.Clients.User(userId)
//            .SendAsync("ReceiveNotification", notification);
//    }

//    // إرسال إشعار لكل المتصلين (Broadcast)
//    public async Task BroadcastNotification(NotificationDto notification)
//    {
//        await _hubContext.Clients.All
//            .SendAsync("ReceiveNotification", notification);
//    }
//}

//[ApiController]
//[Route("api/[controller]")]
//public class NotificationController : ControllerBase
//{
//    private readonly NotificationService _notificationService;

//    public NotificationController(NotificationService notificationService)
//    {
//        _notificationService = notificationService;
//    }

//    [HttpPost("send")]
//    public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
//    {
//        await _notificationService.SendNotification(request.UserId, request.Title, request.Message);
//        return Ok("Notification sent");
//    }

//    [HttpPost("broadcast")]
//    public async Task<IActionResult> Broadcast([FromBody] NotificationRequest request)
//    {
//        await _notificationService.BroadcastNotification(request.Title, request.Message);
//        return Ok("Broadcast sent");
//    }
//}

//public class NotificationRequest
//{
//    public string UserId { get; set; }  // Optional في حالة Broadcast
//    public string Title { get; set; }
//    public string Message { get; set; }
//}
