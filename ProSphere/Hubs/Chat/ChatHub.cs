using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ProSphere.Hubs.Chat
{
    [Authorize]
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            var connectionId = Context.ConnectionId;

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.UserIdentifier;

            Console.WriteLine($"User Disconnected: {userId}");

            await base.OnDisconnectedAsync(exception);
        }

        // 🟢 Broadcast
        public async Task SendMessageToAll(string message)
        {
            var senderId = Context.UserIdentifier;

            await Clients.All.SendAsync(
                "ReceiveMessage",
                senderId,
                message
            );
        }

        public async Task SendPrivateMessage(string receiverUserId, string message)
        {
            var senderUserId = Context.UserIdentifier;

            // ابعت للمستخدم المستلم فقط
            await Clients.User(receiverUserId)
                .SendAsync("ReceivePrivateMessage", senderUserId, message);
        }
    }
}

//using Microsoft.AspNetCore.SignalR;

//public class ChatService
//{
//    private readonly IHubContext<ChatHub> _hubContext;

//    public ChatService(IHubContext<ChatHub> hubContext)
//    {
//        _hubContext = hubContext;
//    }

//    // إرسال رسالة خاصة من User X إلى User Y
//    public async Task SendPrivateMessage(string senderUserId, string receiverUserId, string message)
//    {
//        await _hubContext.Clients.User(receiverUserId)
//            .SendAsync("ReceivePrivateMessage", senderUserId, message);
//    }

//    // إرسال Broadcast لكل Online Users
//    public async Task BroadcastMessage(string senderUserId, string message)
//    {
//        await _hubContext.Clients.All
//            .SendAsync("ReceiveMessage", senderUserId, message);
//    }
//}


//[ApiController]
//[Route("api/[controller]")]
//public class ChatController : ControllerBase
//{
//    private readonly ChatService _chatService;

//    public ChatController(ChatService chatService)
//    {
//        _chatService = chatService;
//    }

//    [HttpPost("private")]
//    public async Task<IActionResult> SendPrivate([FromBody] PrivateMessageRequest request)
//    {
//        // senderUserId ممكن تجي من JWT
//        var senderUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

//        await _chatService.SendPrivateMessage(senderUserId!, request.ReceiverUserId, request.Message);
//        return Ok("Message sent");
//    }

//    [HttpPost("broadcast")]
//    public async Task<IActionResult> Broadcast([FromBody] BroadcastMessageRequest request)
//    {
//        var senderUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

//        await _chatService.BroadcastMessage(senderUserId!, request.Message);
//        return Ok("Broadcast sent");
//    }
//}

//public class PrivateMessageRequest
//{
//    public string ReceiverUserId { get; set; }
//    public string Message { get; set; }
//}

//public class BroadcastMessageRequest
//{
//    public string Message { get; set; }
//}
