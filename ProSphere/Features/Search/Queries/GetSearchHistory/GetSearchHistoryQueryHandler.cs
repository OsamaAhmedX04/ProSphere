using MediatR;
using Microsoft.EntityFrameworkCore;
using ProSphere.RepositoryManager.Interfaces;

namespace ProSphere.Features.Search.Queries.GetSearchHistory
{
    public class GetSearchHistoryQueryHandler : IRequestHandler<GetSearchHistoryQuery, List<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSearchHistoryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<string>> Handle(GetSearchHistoryQuery query, CancellationToken cancellationToken)
        {
            var searchHistories = await _unitOfWork.SearchHistories.GetAllAsIQueryable()
                .Where(sh => sh.UserId == query.userId)
                .OrderByDescending(sh => sh.SearchDate)
                .Take(10)
                .Select(sh => sh.SearchTerm)
                .Where(term => term != null) 
                .ToListAsync(cancellationToken);

            return searchHistories!;
        }
    }
}
