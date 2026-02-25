using ProSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Domain.Entities
{
    public class ReportedProject
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

        [ForeignKey("Reporter")]
        public string? ReporterId { get; set; }
        public ApplicationUser? Reporter { get; set; }

        [ForeignKey("Moderator")]
        public string? ReviewedBy { get; set; }
        public Moderator? Moderator { get; set; }
        public ReportReason Reason { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public DateTime CreatedAt { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
