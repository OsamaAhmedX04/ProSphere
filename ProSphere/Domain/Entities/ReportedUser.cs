using ProSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Domain.Entities
{
    public class ReportedUser
    {
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }

        [ForeignKey("Reporter")]
        public string? ReporterId { get; set; }
        public ApplicationUser? Reporter { get; set; }
        public ReportReason Reason { get; set; }
        public string? Description { get; set; }

        [ForeignKey("Moderator")]
        public string? ReviewedBy { get; set; }
        public Moderator? Moderator { get; set; }
        public Status Status { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public DateTime CreatedAt { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
