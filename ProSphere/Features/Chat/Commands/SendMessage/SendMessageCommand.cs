using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Chat.Commands.SendMessage
{
    public record SendMessageCommand(string senderId, string receiverId, SendMessageRequest request) : IRequest<Result>
    {
    }
}
