using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProSphere.Features.Authentication.Commands.Login;
using ProSphere.Features.CreatorSkills.Commands.AddNewSkills;
using ProSphere.Features.CreatorSkills.Commands.DeleteSkills;
using ProSphere.Features.CreatorSkills.Queries.GetCreatorSkills;
using ProSphere.Features.CreatorSkills.Queries.GetSearchedSkills;
using ProSphere.Features.CreatorSkills.Queries.GetSkillsStatus;

namespace ProSphere.Features.CreatorSkills.Endpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISender _sender;

        public SkillController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("creator/{id}")]
        public async Task<IActionResult> GetCreatorSkills(string id, int pageNumber)
        {
            var query = new GetCreatorSkillsQuery(id, pageNumber);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{searchTerm}")]
        public async Task<IActionResult> GetSearchedSkills(int pageNumber, string searchTerm)
        {
            var query = new GetSearchedSkillsQuery(pageNumber, searchTerm);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetSkillsStatus(int pageNumber)
        {
            var query = new GetSkillsStatusQuery(pageNumber);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("{creatorId}")]
        public async Task<IActionResult> AddSkill(string creatorId, [FromBody] AddNewSkillsRequest request)
        {
            var command = new AddNewSkillsCommand(creatorId, request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{creatorId}")]
        public async Task<IActionResult> DeleteSkill(string creatorId, DeleteSkillsRequest request)
        {
            var command = new DeleteSkillsCommand(creatorId, request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
}
