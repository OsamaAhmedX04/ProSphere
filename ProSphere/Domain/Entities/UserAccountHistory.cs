using System.ComponentModel.DataAnnotations;

namespace ProSphere.Domain.Entities
{
    public class UserAccountHistory
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
