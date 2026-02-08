namespace ProSphere.Features.Verification.Queries.GetProfessionalInvestorVerifications
{
    public class GetProfessionalInvestorVerificationResponse
    {
        public Guid ProfessionalDocumentId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string VerificationType { get; set; } = "Professional Verification";
        public DateTime CreatedAt { get; set; }
    }
}
