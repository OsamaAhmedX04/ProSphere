using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProSphere.Features.Verification.Queries.GetFinancialDocumentTypes;
using ProSphere.Features.VideoCall.Commands.CreateMeeting;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProSphere.Features.VideoCall.Endpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoCallController : ControllerBase
    {

        private readonly ISender _sender;

        public VideoCallController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateMeetingData(string firstUserId, string secondUserId)
        {
            var command = new CreateMeetingCommand(firstUserId, secondUserId);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
}
