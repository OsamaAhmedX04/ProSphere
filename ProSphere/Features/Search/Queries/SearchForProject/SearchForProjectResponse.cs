namespace ProSphere.Features.Search.Queries.SearchForProject
{
    public class SearchForProjectResponse
    {
        public Guid ProjectId { get; set; }
        public string CreatorId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Problem { get; set; }
        public string SolutionSummary { get; set; }
        public string Market { get; set; }
        public decimal NeededInvestment { get; set; }
        public double EquityPercentage { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public bool IsInvested { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
