using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;

namespace ProSphere.Data.Configurations
{
    public class ProjectModerationConfig : IEntityTypeConfiguration<ProjectModeration>
    {
        public void Configure(EntityTypeBuilder<ProjectModeration> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.RowVersion)
                .IsRowVersion();

            builder
                .HasOne(x => x.Project)
                .WithOne(x => x.ModerationAction)
                .HasForeignKey<ProjectModeration>(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.Moderator)
                .WithMany(x => x.ProjectsModerations)
                .HasForeignKey(x => x.ModeratorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
