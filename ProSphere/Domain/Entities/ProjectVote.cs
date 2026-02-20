using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Domain.Entities
{
    public class ProjectVote
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

        [ForeignKey("Creator")]
        public string CreatorId { get; set; }
        public Creator Creator { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
