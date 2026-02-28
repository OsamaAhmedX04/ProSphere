using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProSphere.Features.ProjectModerating.Commands.AcceptProjectCreation;
using ProSphere.Features.ProjectVoting.Commands.AddVote;
using ProSphere.Features.ProjectVoting.Commands.DeleteVote;
using ProSphere.Features.ProjectVoting.Queries.GetAllVotersOnProject;

namespace ProSphere.Features.ProjectVoting.Endpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectVotingController : ControllerBase
    {
        private readonly ISender _sender;

        public ProjectVotingController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("/api/vote/{projectId}")]
        public async Task<IActionResult> GetAllProjectVotes(int pageNumber, Guid projectId)
        {
            var query = new GetAllVotersOnProjectQuery(pageNumber, projectId);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("/api/vote/{projectId}")]
        public async Task<IActionResult> AddVote(string creatorId, Guid projectId)
        {
            var command = new AddVoteCommand(creatorId, projectId);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("/api/vote/{projectId}")]
        public async Task<IActionResult> DeleteVote(string creatorId, Guid projectId)
        {
            var command = new DeleteVoteCommand(creatorId, projectId);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
}
