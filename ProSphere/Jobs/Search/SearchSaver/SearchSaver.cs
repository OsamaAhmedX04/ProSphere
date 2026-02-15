
using ProSphere.Domain.Constants.SearchConstants;
using ProSphere.Domain.Entities;
using ProSphere.RepositoryManager.Interfaces;

namespace ProSphere.Jobs.Search.SearchSaver
{
    public class SearchSaver : ISearchSaver
    {
        private readonly IUnitOfWork _unitOfWork;

        public SearchSaver(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task SaveSearchAtCreatorHistory(string userId, string? userName = null, bool? verified = null)
        {
            var searchHistory = new SearchHistory
            {
                UserId = userId,
                SearchTerm = userName,
                FullSearchTerm = $"UserName: {userName}, Verified: {verified}",
                SearchCategory = SearchCategory.Creator
            };
            await _unitOfWork.SearchHistories.AddAsync(searchHistory);
            await _unitOfWork.CompleteAsync();
        }
        

        public async Task SaveSearchAtInvestorHistory(string userId, string? userName = null, bool? verified = null, bool? financial = null, bool? professional = null)
        {
                var searchHistory = new SearchHistory
                {
                    UserId = userId,
                    SearchTerm = userName,
                    FullSearchTerm = $"UserName: {userName}, Verified: {verified}, Financial: {financial}, Professional: {professional}",
                    SearchCategory = SearchCategory.Investors
                };
                await _unitOfWork.SearchHistories.AddAsync(searchHistory);
            await _unitOfWork.CompleteAsync();
        }
    }
}
