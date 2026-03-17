using MediatR;
using Microsoft.EntityFrameworkCore;
using ProSphere.ExternalServices.Interfaces.JWT;
using ProSphere.RepositoryManager.Interfaces;

namespace ProSphere.Features.Search.Queries.GetSearchHistory
{
    public class GetSearchHistoryQueryHandler : IRequestHandler<GetSearchHistoryQuery, List<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public GetSearchHistoryQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<List<string>> Handle(GetSearchHistoryQuery query, CancellationToken cancellationToken)
        {
            var searchHistories = await _unitOfWork.SearchHistories.GetAllAsIQueryable()
                .Where(sh => sh.UserId == _currentUser.UserId)
                .OrderByDescending(sh => sh.SearchDate)
                .Take(10)
                .Select(sh => sh.SearchTerm)
                .Where(term => term != null)
                .ToListAsync(cancellationToken);

            return searchHistories!;
        }
    }
}
