using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProSphere.Features.Search.Queries.GetSearchHistory;
using ProSphere.Features.Search.Queries.SearchForCreators;
using ProSphere.Features.Search.Queries.SearchForInvestors;
using System.Security.Claims;

namespace ProSphere.Features.Search.Endpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISender _sender;

        public SearchController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("creators")]
        public async Task<IActionResult> SearchForCreators
            (int pageNumber, string? userName = null, bool? verified = null)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = new SearchForCreatorsQuery(pageNumber, userId, userName, verified);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("investors")]
        public async Task<IActionResult> SearchForInvestors
            (int pageNumber, string? userName = null, bool? verified = null, bool? financial = null, bool? professional = null)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = new SearchForInvestorsQuery(pageNumber, userId, userName, verified, financial, professional);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("history")]
        public async Task<IActionResult> SearchForInvestors()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = new GetSearchHistoryQuery(userId!);
            var result = await _sender.Send(query);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
