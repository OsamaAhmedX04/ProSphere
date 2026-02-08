namespace ProSphere.Features.Verification.Queries.GetFinancialInvestorVerifications
{
    public class GetFinancialInvestorVerificationResponse
    {
        public Guid FinancialDocumentId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string VerificationType { get; set; } = "Financial Verification";
        public DateTime CreatedAt { get; set; }
    }
}
