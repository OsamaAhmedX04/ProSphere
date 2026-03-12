using Newtonsoft.Json;

namespace ProSphere.Features.VideoCall.Commands.CreateMeeting
{
    public class CreateMeetingResponse
    {
        public long Id { get; set; }

        [JsonProperty("join_url")]
        public string JoinUrl { get; set; }

        [JsonProperty("start_url")]
        public string StartUrl { get; set; }

        public string Password { get; set; }
    }
}
