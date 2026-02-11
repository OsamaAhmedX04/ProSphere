using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProSphere.Features.Registration.Commands.ConfirmEmail;
using ProSphere.Features.Registration.Commands.Register;
using ProSphere.Features.Registration.Commands.ResendConfirmEmailToken;

namespace ProSphere.Features.Registration.Endpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ISender _sender;

        public RegistrationController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = new RegisterCommand(request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var command = new ConfirmEmailCommand(userId, token);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("resend-token")]
        public async Task<IActionResult> ResendConfirmEmailToken(string email)
        {
            var command = new ResendConfirmEmailTokenCommand(email);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
}
