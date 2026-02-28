using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProSphere.Features.ProjectManagement.Queries.GetCreatorProjectData;
using ProSphere.Features.Reports.Commands.AcceptReportOnProject;
using ProSphere.Features.Reports.Commands.AcceptReportOnUser;
using ProSphere.Features.Reports.Commands.RejectReportOnProject;
using ProSphere.Features.Reports.Commands.RejectReportOnUser;
using ProSphere.Features.Reports.Commands.SendReportOnProject;
using ProSphere.Features.Reports.Commands.SendReportOnUser;
using ProSphere.Features.Reports.Queries.GetAllProjectsReports;
using ProSphere.Features.Reports.Queries.GetAllUsersReports;
using ProSphere.Features.Reports.Queries.GetReportReasons;

namespace ProSphere.Features.Reports.Endpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ISender _sender;

        public ReportController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("reasons")]
        public async Task<IActionResult> GetReportReasons()
        {
            var query = new GetReportReasonsQuery();
            var result = await _sender.Send(query);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("projects")]
        public async Task<IActionResult> GetAllProjectsReports(int pageNumber)
        {
            var query = new GetAllProjectsReportsQuery(pageNumber);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUserssReports(int pageNumber)
        {
            var query = new GetAllUsersReportsQuery(pageNumber);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("project/{projectId}")]
        public async Task<IActionResult> SendReportOnProject(string reporterId, Guid projectId, SendReportOnProjectRequest request)
        {
            var command = new SendReportOnProjectCommand(reporterId, projectId, request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("user/{userId}")]
        public async Task<IActionResult> SendReportOnUser(string reporterId, string targetUserId, SendReportOnUserRequest request)
        {
            var command = new SendReportOnUserCommand(reporterId, targetUserId, request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("project/{reportId}/accept")]
        public async Task<IActionResult> AcceptReportOnProject(string moderatorId, Guid reportId)
        {
            var command = new AcceptReportOnProjectCommand(moderatorId, reportId);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("user/{reportId}/accept")]
        public async Task<IActionResult> AcceptReportOnUser(string moderatorId, Guid reportId, int numberOfBanDays)
        {
            var command = new AcceptReportOnUserCommand(moderatorId, reportId, numberOfBanDays);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("project/{reportId}/reject")]
        public async Task<IActionResult> RejectReportOnProject(string moderatorId, Guid reportId)
        {
            var command = new RejectReportOnProjectCommand(moderatorId, reportId);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("user/{reportId}/reject")]
        public async Task<IActionResult> RejectReportOnUser(string moderatorId, Guid reportId)
        {
            var command = new RejectReportOnUserCommand(moderatorId, reportId);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
}
