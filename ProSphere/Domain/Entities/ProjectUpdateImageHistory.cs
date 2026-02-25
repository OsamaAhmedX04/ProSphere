using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Domain.Entities
{
    public class ProjectUpdateImageHistory
    {
        [Key]
        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }
        public ProjectUpdateHistory Project { get; set; }
        public string ImageUrl { get; set; }
    }
}
