using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProSphere.Features.Admin.Commands.CreateAdmin;
using ProSphere.Features.Authentication.Commands.Login;
using ProSphere.Features.Authentication.Commands.RefreshToken;

namespace ProSphere.Features.Authentication.Endpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISender _sender;

        public AuthController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var command = new LoginCommand(request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var command = new RefreshTokenCommand(request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
}
