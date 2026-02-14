using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProSphere.Features.Account.Commands.DeleteAccount;
using ProSphere.Features.Account.Commands.RequestDeleteAccount;
using ProSphere.Features.Account.Commands.UpdateCreatorAccount;
using ProSphere.Features.Account.Commands.UpdateInvestorAccount;
using ProSphere.Features.Account.Queries.GetAdminAccount;
using ProSphere.Features.Account.Queries.GetCreatorAccount;
using ProSphere.Features.Account.Queries.GetInvestorAccount;
using ProSphere.Features.Account.Queries.GetModeratorAccounts;

namespace ProSphere.Features.Account.Endpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ISender _sender;

        public AccountController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("admin/{id}")]
        public async Task<IActionResult> GetAdminAccount(string id)
        {
            var query = new GetAdminAccountQuery(id);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("investor/{id}")]
        public async Task<IActionResult> GetInvestorAccount(string id)
        {
            var query = new GetInvestorAccountQuery(id);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("Creator/{id}")]
        public async Task<IActionResult> GetCreatorAccount(string id)
        {
            var query = new GetCreatorAccountQuery(id);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("moderators")]
        public async Task<IActionResult> GetModeratorAccounts(int pageNumber, string? userName = null)
        {
            var query = new GetModeratorAccountsQuery(pageNumber, userName);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("creator/{creatorId}")]
        public async Task<IActionResult> UpdateCreatorAccount(string creatorId, [FromForm] UpdateCreatorAccountRequest request)
        {
            var command = new UpdateCreatorAccountCommand(creatorId, request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("investor/{investorId}")]
        public async Task<IActionResult> UpdateInvestorAccount(string investorId, [FromForm] UpdateInvestorAccountRequest request)
        {
            var command = new UpdateInvestorAccountCommand(investorId, request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("delete-request/{id}")]
        public async Task<IActionResult> RequestDeleteAccount(string id)
        {
            var command = new RequestDeleteAccountCommand(id);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(string id, string otp)
        {
            var command = new DeleteAccountCommand(id, otp);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
}
