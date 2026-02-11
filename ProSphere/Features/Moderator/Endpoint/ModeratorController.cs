using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProSphere.Features.CreatorSkills.Commands.DeleteSkills;
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
