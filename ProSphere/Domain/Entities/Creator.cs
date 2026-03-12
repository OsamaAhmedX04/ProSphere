using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Domain.Entities
{
    public class Creator
    {
        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }
        public ApplicationUser User { get; set; }
        public string FullName { get; set; }
        public string? ImageProfileURL { get; set; }
        public string? HeadLine { get; set; }
        public string? BIO { get; set; }
        public string? CVURL { get; set; }
        public ICollection<CreatorSkill> Skills { get; set; } = new List<CreatorSkill>();
        public ICollection<Project> Projects { get; set; } = new List<Project>();
        public ICollection<ProjectVote> ProjectsVotes { get; set; } = new List<ProjectVote>();
        public ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();
        public ICollection<Meeting> Meetings { get; set; } = new List<Meeting>();
    }
}
