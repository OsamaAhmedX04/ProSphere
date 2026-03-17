using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProSphere.Features.Search.Queries.GetSearchHistory;
using ProSphere.Features.Search.Queries.SearchForCreators;
using ProSphere.Features.Search.Queries.SearchForInvestors;
using ProSphere.Features.Search.Queries.SearchForProject;
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
            var query = new SearchForCreatorsQuery(pageNumber, userName, verified);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("investors")]
        public async Task<IActionResult> SearchForInvestors
            (int pageNumber, string? userName = null, bool? verified = null, bool? financial = null, bool? professional = null)
        {
            var query = new SearchForInvestorsQuery(pageNumber, userName, verified, financial, professional);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("projects")]
        public async Task<IActionResult> SearchForProjects
            (int pageNumber, string? projectName = null)
        {
            var query = new SearchForProjectQuery(pageNumber, projectName);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistoryOfSearch()
        {
            var query = new GetSearchHistoryQuery();
            var result = await _sender.Send(query);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
