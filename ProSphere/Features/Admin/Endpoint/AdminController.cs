using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProSphere.Features.Account.Queries.GetAdminAccount;
using ProSphere.Features.Admin.Commands.CreateAdmin;
using ProSphere.Features.Admin.Commands.DeleteAdmin;

namespace ProSphere.Features.Admin.Endpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ISender _sender;

        public AdminController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdmin([FromBody] CreateAdminRequest request)
        {
            var command = new CreateAdminCommand(request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(string id)
        {
            var command = new DeleteAdminCommand(id);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
}
