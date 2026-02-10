using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Domain.Entities
{
    public class CreatorSkill
    {
        [ForeignKey("Creator")]
        public string CreatorId { get; set; }
        public Creator Creator { get; set; }

        [ForeignKey("Skill")]
        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
