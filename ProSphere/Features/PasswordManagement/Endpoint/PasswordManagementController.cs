using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProSphere.Features.PasswordManagement.Commands.ChangeInActiveRolePassword;
using ProSphere.Features.PasswordManagement.Commands.ChangePassword;

namespace ProSphere.Features.PasswordManagement.Endpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordManagementController : ControllerBase
    {
        private readonly ISender _sender;

        public PasswordManagementController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPut("/api/password-management")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var command = new ChangePasswordCommand(request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("/api/password-management/change-inactive-role")]
        public async Task<IActionResult> ChangeInActiveRolePassword(ChangeInActiveRolePasswordRequest request)
        {
            var command = new ChangeInActiveRolePasswordCommand(request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
}
