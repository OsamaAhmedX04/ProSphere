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
        public string UserName { get; set; }
        public string? ImageProfileURL { get; set; }
    }
}
