using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Domain.Entities
{
    public class ProjectUpdateImageHistory
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("ProjectUpdateHistory")]
        public Guid ProjectUpdateHistoryId { get; set; }
        public ProjectUpdateHistory ProjectUpdateHistory { get; set; }
        public string ImageUrl { get; set; }
    }
}
