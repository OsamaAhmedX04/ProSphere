using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProSphere.Features.Moderator.Queries.GetModeratorsAvailableEmail;
using ProSphere.Features.ProjectManagement.Commands.CreateProject;
using ProSphere.Features.ProjectManagement.Commands.DeleteProject;
using ProSphere.Features.ProjectManagement.Commands.UpdateProject;
using ProSphere.Features.ProjectManagement.Queries.GetCreatorProjectData;
using ProSphere.Features.ProjectManagement.Queries.GetCreatorProjectDetails;
using ProSphere.Features.ProjectManagement.Queries.GetCreatorProjects;
using ProSphere.Features.ProjectManagement.Queries.GetCreatorProjectUpdateVersion;

namespace ProSphere.Features.ProjectManagement.Endpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ISender _sender;

        public ProjectController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("/api/project/{creatorId}")]
        public async Task<IActionResult> GetCreatorProjects(string creatorId, int pageNumber, string? title = null, string? status = null)
        {
            var query = new GetCreatorProjectsQuery(creatorId, pageNumber, title, status);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("/api/project/{creatorId}/{projectId}")]
        public async Task<IActionResult> GetCreatorProjectData(string creatorId, Guid projectId)
        {
            var query = new GetCreatorProjectDataQuery(creatorId ,projectId);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("/api/project/{creatorId}/{projectId}/details")]
        public async Task<IActionResult> GetCreatorProjectDetails(string creatorId, Guid projectId)
        {
            var query = new GetCreatorProjectDetailsQuery(creatorId, projectId);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("/api/project/{creatorId}/{projectId}/update-version")]
        public async Task<IActionResult> GetCreatorProjectUpdateVersion(string creatorId, Guid projectId)
        {
            var query = new GetCreatorProjectUpdateVersionQuery(creatorId, projectId);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("/api/project")]
        public async Task<IActionResult> CreateProject(string creatorId, CreateProjectRequest request)
        {
            var command = new CreateProjectCommand(creatorId, request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("/api/project/{projectId}")]
        public async Task<IActionResult> UpdateProject(string creatorId, Guid projectId, UpdateProjectRequest request)
        {
            var command = new UpdateProjectCommand(creatorId, projectId, request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("/api/project/{projectId}")]
        public async Task<IActionResult> DeleteProject(string creatorId, Guid projectId)
        {
            var command = new DeleteProjectCommand(creatorId, projectId);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
}
