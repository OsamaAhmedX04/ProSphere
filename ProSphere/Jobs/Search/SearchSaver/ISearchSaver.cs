namespace ProSphere.Jobs.Search.SearchSaver
{
    public interface ISearchSaver
    {
        Task SaveSearchAtCreatorHistory(string userId, string? userName = null, bool? verified = null);
        Task SaveSearchAtInvestorHistory(string userId, string? userName = null, bool? verified = null, bool? financial = null,
        bool? professional = null);
    }
}
