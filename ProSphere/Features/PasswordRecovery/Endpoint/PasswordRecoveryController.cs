using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProSphere.Features.PasswordManagement.Commands.ChangePassword;
using ProSphere.Features.PasswordRecovery.Commands.ForgotPasswrod;
using ProSphere.Features.PasswordRecovery.Commands.ResendResetPasswordToken;
using ProSphere.Features.PasswordRecovery.Commands.ResetPassword;

namespace ProSphere.Features.PasswordRecovery.Endpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordRecoveryController : ControllerBase
    {
        private readonly ISender _sender;

        public PasswordRecoveryController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("/api/password-recovery/forgot")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var command = new ForgotPasswordCommand(email);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("/api/password-recovery/reset")]
        public async Task<IActionResult> ResetPassword(string userId, string token, ResetPasswordRequest request)
        {
            var command = new ResetPasswordCommand(userId, token, request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("/api/password-recovery/resend-token")]
        public async Task<IActionResult> ResendResetPasswordToken(string email)
        {
            var command = new ResendResetPasswordTokenCommand(email);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
}
