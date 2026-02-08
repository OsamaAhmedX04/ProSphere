namespace ProSphere.Features.Verification.Queries.GetIdentityVerificationById
{
    public class GetIdentityVerificationByIdResponse
    {
        public Guid IdentityDocumentId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string IdFrontImageURL { get; set; }
        public string IdBackImageURL { get; set; }
        public string SelfieWithIdURL { get; set; }
        public string status { get; set; }
        public string VerificationType { get; set; } = "Identity Verification";
        public DateTime CreatedAt { get; set; }

    }
}
