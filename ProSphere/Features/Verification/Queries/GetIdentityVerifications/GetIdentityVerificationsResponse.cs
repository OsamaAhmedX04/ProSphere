namespace ProSphere.Features.Verification.Queries.GetIdentityVerifications
{
    public class GetIdentityVerificationResponse
    {
        public Guid IdentityDocumentId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string VerificationType { get; set; } = "Identity Verification";
        public DateTime CreatedAt { get; set; }

    }
}
