using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProSphere.Features.Registration.Commands.Register;
using ProSphere.Features.SocialMediaAccounts.Commands.SetSocialMediaAccounts;
using ProSphere.Features.SocialMediaAccounts.Queries.GetUserSocialMeidaAccounts;

namespace ProSphere.Features.SocialMediaAccounts.Endpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaAccountsController : ControllerBase
    {
        private readonly ISender _sender;

        public SocialMediaAccountsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserSocialMediaAccounts(string userId)
        {
            var query = new GetUserSocialMeidaAccountsQuery(userId);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> SetUserSocialMediaAccounts(string userId, SetSocialMediaAccountsRequest request)
        {
            var command = new SetSocialMediaAccountsCommand(userId, request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
}
