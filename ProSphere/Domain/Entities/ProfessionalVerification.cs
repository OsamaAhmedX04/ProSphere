using ProSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Domain.Entities
{
    public class ProfessionalVerification
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Investor")]
        public string InvestorId { get; set; }
        public Investor Investor { get; set; }

        [ForeignKey("DocumentType")]
        public int DocumentTypeId { get; set; }
        public ProfessionalDocumentType DocumentType { get; set; }
        public string DocumentURL { get; set; }

        public string? Notes { get; set; }

        public Status status { get; set; }
        public string? RejectionReason { get; set; }

        [ForeignKey("Moderator")]
        public string? ReviewedBy { get; set; }
        public Moderator? Moderator { get; set; }
        public DateTime ReviewedAt { get; set; }
        public DateTime CreatedAt { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
