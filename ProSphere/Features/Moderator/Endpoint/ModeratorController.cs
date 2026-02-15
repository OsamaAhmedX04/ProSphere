using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProSphere.Features.Moderator.Commands.CreateModerator;
using ProSphere.Features.Moderator.Commands.RecycleModeratorAccount;

namespace ProSphere.Features.Moderator.Endpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModeratorController : ControllerBase
    {
        private readonly ISender _sender;

        public ModeratorController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("emails")]
        public async Task<IActionResult> GetModeratorsEmail(int pageNumber)
        {
            var query = new GetModeratorsEmailQuery(pageNumber);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateModerator(CreateModeratorRequest request)
        {
            var command = new CreateModeratorCommand(request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{moderatorId}")]
        public async Task<IActionResult> RecycleModeratorAccount(string moderatorId)
        {
            var command = new RecycleModeratorAccountCommand(moderatorId);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
}
