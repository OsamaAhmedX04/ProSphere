using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProSphere.Features.Chat.Commands.SendMessage;
using ProSphere.Features.Chat.Queries.GetAllChatBetweenUsers;
using ProSphere.Features.Chat.Queries.GetAllContactsForUser;

namespace ProSphere.Features.Chat.Endpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ISender _sender;

        public ChatController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("contacts")]
        public async Task<IActionResult> GetUserContacts(int pageNumber, string userId, string? contactName = null)
        {
            var query = new GetAllContactsForUserQuery(pageNumber, userId, contactName);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("messages")]
        public async Task<IActionResult> GetChatContent(string firstUserId, string secondUserId, int pageNumber)
        {
            var query = new GetAllChatBetweenUsersQuery(firstUserId, secondUserId, pageNumber);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("message")]
        public async Task<IActionResult> SendMessage(string senderId, string receiverId, [FromForm] SendMessageRequest request)
        {
            var command = new SendMessageCommand(senderId, receiverId, request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
}
