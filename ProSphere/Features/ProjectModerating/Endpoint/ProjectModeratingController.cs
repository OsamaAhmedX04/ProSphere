using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProSphere.Features.ProjectModerating.Commands.AcceptProjectCreation;
using ProSphere.Features.ProjectModerating.Commands.RejectProjectCreation;
using ProSphere.Features.ProjectModerating.Queries.GetAllPendingProjects;
using ProSphere.Features.ProjectModerating.Queries.GetAllUpdatedPendingProjects;
using ProSphere.Features.ProjectModerating.Queries.GetPendingProjectDetails;
using ProSphere.Features.ProjectModerating.Queries.GetUpdatedPendingProjectDetails;

namespace ProSphere.Features.ProjectModerating.Endpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectModeratingController : ControllerBase
    {
        private readonly ISender _sender;

        public ProjectModeratingController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("/api/project/pending")]
        public async Task<IActionResult> GetAllPendingProjects(int pageNumber)
        {
            var query = new GetAllPendingProjectsQuery(pageNumber);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("/api/project/pending/{projectId}/details")]
        public async Task<IActionResult> GetPendingProjectDetails(Guid projectId)
        {
            var query = new GetPendingProjectDetailsQuery(projectId);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("/api/updated-project/pending")]
        public async Task<IActionResult> GetAllUpdatedPendingProjects(int pageNumber)
        {
            var query = new GetAllUpdatedPendingProjectsQuery(pageNumber);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("/api/updated-project/pending/{projectId}/details")]
        public async Task<IActionResult> GetUpdatedPendingProjectDetails(Guid projectId)
        {
            var query = new GetUpdatedPendingProjectDetailsQuery(projectId);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("/api/project/{projectId}/accept")]
        public async Task<IActionResult> AcceptProject(string moderatorId, Guid projectId)
        {
            var command = new AcceptProjectCreationCommand(moderatorId, projectId);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("/api/project/{projectId}/reject")]
        public async Task<IActionResult> RejectProject(string moderatorId, Guid projectId, RejectProjectCreationRequest request)
        {
            var command = new RejectProjectCreationCommand(moderatorId, projectId, request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
}
