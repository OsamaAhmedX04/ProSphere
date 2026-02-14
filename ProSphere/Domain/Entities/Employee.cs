using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Domain.Entities
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }

        [ForeignKey("Moderator")]
        public string? AssignedTo { get; set; }
        public Moderator? Moderator { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartWorkAt { get; set; }
        public DateTime? EndWorkAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
