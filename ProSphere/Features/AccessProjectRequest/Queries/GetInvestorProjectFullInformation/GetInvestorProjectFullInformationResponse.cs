namespace ProSphere.Features.AccessProjectRequest.Queries.GetInvestorProjectFullInformation
{
    public class GetInvestorProjectFullInformationResponse
    {
        public Guid ProjectId { get; set; }
        public string CreatorId { get; set; }
        public string CreatorFullName { get; set; }
        public string CreatorUserName { get; set; }
        public string? HeadLine { get; set; }
        public bool IsVerified { get; set; }
        public string? CreatorImageProfile { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Problem { get; set; }
        public string SolutionSummary { get; set; }
        public string Market { get; set; }
        public decimal NeededInvestment { get; set; }
        public double EquityPercentage { get; set; }
        public bool IsActive { get; set; }
        public bool IsInvested { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsBlockedDueToBannedUser { get; set; }
        public List<string> ImagesURL { get; set; } = new List<string>();
        public string ExecutionPlan { get; set; }
        public string FinancialDetails { get; set; }
        public string BusinessModel { get; set; }
        public string MarketingStrategy { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
