namespace ProSphere.Features.ProjectModerating.Queries.GetUpdatedPendingProjectDetails
{
    public class GetUpdatedPendingProjectDetailsResponse
    {
        public string ExecutionPlan { get; set; }
        public string FinancialDetails { get; set; }
        public string BusinessModel { get; set; }
        public string MarketingStrategy { get; set; }
        public string? Notes { get; set; }
    }
}
