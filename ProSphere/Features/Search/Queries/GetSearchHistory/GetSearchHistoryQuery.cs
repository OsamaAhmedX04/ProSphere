using MediatR;

namespace ProSphere.Features.Search.Queries.GetSearchHistory
{
    public record GetSearchHistoryQuery(string userId) : IRequest<List<string>>
    {
    }
}
