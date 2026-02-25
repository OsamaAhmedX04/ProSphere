namespace ProSphere.Features.ProjectModerating.Queries.GetAllPendingProjects
{
    public class GetAllPendingProjectsResponse
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Problem { get; set; }
        public string SolutionSummary { get; set; }
        public string Market { get; set; }
        public decimal NeededInvestment { get; set; }
        public double EquityPercentage { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public bool IsInvested { get; set; }
        public List<string> ImagesURL { get; set; } = new List<string>();
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
