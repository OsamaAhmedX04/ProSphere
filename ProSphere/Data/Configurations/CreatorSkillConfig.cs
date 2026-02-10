using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class CreatorSkillConfig : IEntityTypeConfiguration<CreatorSkill>
    {
        public void Configure(EntityTypeBuilder<CreatorSkill> builder)
        {
            builder.HasKey(cs => new { cs.CreatorId, cs.SkillId });

            builder.HasOne(cs => cs.Creator)
                .WithMany(c => c.Skills)
                .HasForeignKey(cs => cs.CreatorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cs => cs.Skill)
                .WithMany(s => s.Creators)
                .HasForeignKey(cs => cs.SkillId)
                .OnDelete(DeleteBehavior.Cascade); ;
        }
    }
}
