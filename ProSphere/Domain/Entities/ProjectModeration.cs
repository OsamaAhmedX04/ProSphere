using ProSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Domain.Entities
{
    public class ProjectModeration
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

        [ForeignKey("Moderator")]
        public string ModeratorId { get; set; }
        public Moderator Moderator { get; set; }
        public Status Status { get; set; }
        public bool IsUpdate { get; set; }
        public string? Reason { get; set; }
        public DateTime CreatedAt { get; set; }


    }
}
