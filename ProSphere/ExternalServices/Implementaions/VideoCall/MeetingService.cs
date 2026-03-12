using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using ProSphere.ExternalServices.Interfaces.VideoCall;
using ProSphere.Features.VideoCall.Commands.CreateMeeting;
using ProSphere.Options;
using System.Net.Http.Headers;
using System.Text;

namespace ProSphere.ExternalServices.Implementaions.VideoCall
{
    public class MeetingService : IMeetingService
    {
        private readonly HttpClient _httpClient;
        private readonly ZoomOptions _settings;

        public MeetingService(HttpClient httpClient, IOptions<ZoomOptions> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }

        public async Task<CreateMeetingResponse> CreateMeeting()
        {
            var token = await GetAccessToken();

            var meeting = new
            {
                topic = "Investor Meeting",
                type = 1,
                settings = new
                {
                    host_video = true,
                    participant_video = true,
                    join_before_host = true,
                    waiting_room = false,
                    mute_upon_entry = false,
                    approval_type = 0,
                    audio = "both",
                    auto_recording = "none"
                }
            };

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://api.zoom.us/v2/users/me/meetings"
            );

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            request.Content = new StringContent(
                JsonConvert.SerializeObject(meeting),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            var meetingResponse =
                JsonConvert.DeserializeObject<CreateMeetingResponse>(json);

            return meetingResponse;
        }

        private async Task<string> GetAccessToken()
        {
            var url =
            $"https://zoom.us/oauth/token?grant_type=account_credentials&account_id={_settings.AccountId}";

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            var auth = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{_settings.ClientId}:{_settings.ClientSecret}")
            );

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Basic", auth);

            var response = await _httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            dynamic data = JsonConvert.DeserializeObject(json);

            return data.access_token;
        }
    }
}
