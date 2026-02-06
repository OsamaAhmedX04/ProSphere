using ProSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Domain.Entities
{
    public class IdentityVerification
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string IdFrontImageURL { get; set; }
        public string IdBackImageURL { get; set; }
        public string SelfieWithIdURL { get; set; }

        public Status status { get; set; }
        public string? RejectionReason { get; set; }

        [ForeignKey("Moderator")]
        public string? ReviewedBy { get; set; }
        public ApplicationUser? Moderator { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
