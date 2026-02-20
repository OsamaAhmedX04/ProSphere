using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Domain.Entities
{
    public class ProjectDetail
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public string ExecutionPlan { get; set; }
        public string FinancialDetails { get; set; }
        public string BusinessModel { get; set; }
        public string MarketingStrategy { get; set; }
        public string? Notes { get; set; }
    }
}
