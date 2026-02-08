using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Features.Verification.Queries.GetProfessionalInvestorVerificationById
{
    public class GetProfessionalInvestorVerificationByIdResponse
    {
        public Guid ProfessionalDocumentId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentURL { get; set; }
        public string? Notes { get; set; }
        public string status { get; set; }
        public string VerificationType { get; set; } = "Professional Verification";
        public DateTime CreatedAt { get; set; }
    }
}
