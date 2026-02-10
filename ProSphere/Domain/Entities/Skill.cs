using System.ComponentModel.DataAnnotations;

namespace ProSphere.Domain.Entities
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CreatorSkill> Creators { get; set; } = new List<CreatorSkill>();
    }
}
