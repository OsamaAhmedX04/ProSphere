using ProSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Domain.Entities
{
    public class Project
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Creator")]
        public string CreatorId { get; set; }
        public Creator Creator { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Problem { get; set; }
        public string SolutionSummary { get; set; }
        public string Market { get; set; }
        public decimal NeededInvestment { get; set; }
        public double EquityPercentage { get; set; }
        public Status Status { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ProjectDetail Details { get; set; }
        public ProjectModeration ModerationAction { get; set; }
        public ICollection<ProjectImage> Images { get; set; } = new List<ProjectImage>();
        public ICollection<ProjectAccessRequest> AccessRequests { get; set; } = new List<ProjectAccessRequest>();
        public ICollection<ProjectVote> Votes { get; set; } = new List<ProjectVote>();
    }
}
