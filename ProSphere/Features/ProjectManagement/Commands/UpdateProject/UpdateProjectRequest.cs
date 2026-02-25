namespace ProSphere.Features.ProjectManagement.Commands.UpdateProject
{
    public class UpdateProjectRequest
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Problem { get; set; }
        public string SolutionSummary { get; set; }
        public string Market { get; set; }
        public decimal NeededInvestment { get; set; }
        public double EquityPercentage { get; set; }

        public List<IFormFile>? Images { get; set; }

        public string ExecutionPlan { get; set; }
        public string FinancialDetails { get; set; }
        public string BusinessModel { get; set; }
        public string MarketingStrategy { get; set; }
        public string? Notes { get; set; }
    }
}
