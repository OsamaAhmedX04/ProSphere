using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;

namespace ProSphere.Features.ProjectManagement.Queries.GetCreatorProjectUpdateVersion
{
    public class GetCreatorProjectUpdateVersionResponse
    {
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Problem { get; set; }
        public string SolutionSummary { get; set; }
        public string Market { get; set; }
        public decimal NeededInvestment { get; set; }
        public double EquityPercentage { get; set; }
        public string ExecutionPlan { get; set; }
        public string FinancialDetails { get; set; }
        public string BusinessModel { get; set; }
        public string MarketingStrategy { get; set; }
        public string? Notes { get; set; }
        public List<string>? ImagesURL { get; set; }
        public string Status { get; set; }
        public string? RejectionReason { get; set; }
    }
}
