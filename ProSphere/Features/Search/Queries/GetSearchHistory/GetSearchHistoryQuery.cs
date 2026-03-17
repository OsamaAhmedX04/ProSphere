using MediatR;

namespace ProSphere.Features.Search.Queries.GetSearchHistory
{
    public record GetSearchHistoryQuery() : IRequest<List<string>>
    {
    }
}
