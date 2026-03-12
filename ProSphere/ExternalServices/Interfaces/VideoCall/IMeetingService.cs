using ProSphere.Features.VideoCall.Commands.CreateMeeting;

namespace ProSphere.ExternalServices.Interfaces.VideoCall
{
    public interface IMeetingService
    {
        Task<CreateMeetingResponse> CreateMeeting();
    }
}
