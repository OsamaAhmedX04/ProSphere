using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.VideoCall.Commands.CreateMeeting
{
    public record CreateMeetingCommand(string firstUserId, string secondUserId) : IRequest<Result<CreateMeetingResponse>>
    {
    }
}
