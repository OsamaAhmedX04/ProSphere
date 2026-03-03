using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProSphere.Features.AccessProjectRequest.Commands.AcceptAccessProjectRequest;
using ProSphere.Features.AccessProjectRequest.Commands.DeleteAccessProjectRequest;
using ProSphere.Features.AccessProjectRequest.Commands.RejectAccessProjectRequest;
using ProSphere.Features.AccessProjectRequest.Commands.SendAccessProjectRequest;
using ProSphere.Features.AccessProjectRequest.Queries.GetAccessRequestsOnProject;
using ProSphere.Features.AccessProjectRequest.Queries.GetAllCreatorAccessedProjectRequests;
using ProSphere.Features.AccessProjectRequest.Queries.GetAllInvestorAccessProjectRequests;
using ProSphere.Features.AccessProjectRequest.Queries.GetInvestorProjectFullInformation;

namespace ProSphere.Features.AccessProjectRequest.Endpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectAccessRequestController : ControllerBase
    {
        private readonly ISender _sender;

        public ProjectAccessRequestController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("/api/access-request/creator/{creatorId}")]
        public async Task<IActionResult> GetAllCreatorProjectsAccessRequests(int pageNumber, string creatorId, string? status = null)
        {
            var query = new GetAllCreatorAccessedProjectRequestsQuery(pageNumber, creatorId, status);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("/api/access-request/project/{projectId}")]
        public async Task<IActionResult> GetAllProjectAccessRequests(int pageNumber, string creatorId, Guid projectId, string? status = null)
        {
            var query = new GetAccessRequestsOnProjectQuery(pageNumber, creatorId, projectId, status);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("/api/access-request/investor/{investorId}")]
        public async Task<IActionResult> GetAllInvestorProjectsAccessRequests(int pageNumber, string investorId, string? status = null)
        {
            var query = new GetAllInvestorAccessProjectRequestsQuery(pageNumber, investorId, status);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("/api/access-request/investor/{investorId}/project/{projectId}")]
        public async Task<IActionResult> GetInvestorProjectFullInformation(string investorId, Guid projectId)
        {
            var query = new GetInvestorProjectFullInformationQuery(investorId, projectId);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("/api/access-request/project/{projectId}")]
        public async Task<IActionResult> SendAccessProjectRequest(string investorId, Guid projectId, SendAccessProjectRequestRequest request)
        {
            var command = new SendAccessProjectRequestCommand(investorId, projectId, request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("/api/access-request/request/{requestId}/accept")]
        public async Task<IActionResult> AcceptAccessProjectRequest(Guid requestId)
        {
            var command = new AcceptAccessProjectRequestCommand(requestId);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("/api/access-request/request/{requestId}/reject")]
        public async Task<IActionResult> RejectAccessProjectRequest(Guid requestId)
        {
            var command = new RejectAccessProjectRequestCommand(requestId);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("/api/access-request/request/{requestId}")]
        public async Task<IActionResult> DeleteAccessProjectRequest(Guid requestId)
        {
            var query = new DeleteAccessProjectRequestCommand(requestId);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }
    }
}
