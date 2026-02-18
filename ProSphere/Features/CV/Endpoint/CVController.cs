using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProSphere.Features.CV.Commands.DeleteCV;
using ProSphere.Features.CV.Commands.UploadCV;
using ProSphere.Features.CV.Queries.GetCVByUserId;

namespace ProSphere.Features.CV.Endpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class CVController : ControllerBase
    {
        private readonly ISender _sender;

        public CVController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserCV(string userId)
        {
            var query = new GetCVByUserIdQuery(userId);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> UploadCV(string userId, [FromForm] UploadCVRequest request)
        {
            var command = new UploadCVCommand(userId, request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteCV(string userId)
        {
            var command = new DeleteCVCommand(userId);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
}
