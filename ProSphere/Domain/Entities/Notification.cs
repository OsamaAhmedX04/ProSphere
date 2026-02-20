using ProSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Domain.Entities
{
    public class Notification
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public NotificationStatus Status { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime SentAt { get; set; }
        public DateTime? SeenAt { get; set; }
        public bool IsSeen { get; set; }
    }
}
