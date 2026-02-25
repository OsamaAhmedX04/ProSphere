using ProSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Domain.Entities
{
    public class BannedUser
    {
        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ReportReason Reason { get; set; }
        public int NumberOfBannedDays {  get; set; }
        public DateTime BannedAt { get; set; }
        public bool IsExpired { get; set; }
    }
}
