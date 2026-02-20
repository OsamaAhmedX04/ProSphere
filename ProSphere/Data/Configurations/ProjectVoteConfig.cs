using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class ProjectVoteConfig : IEntityTypeConfiguration<ProjectVote>
    {
        public void Configure(EntityTypeBuilder<ProjectVote> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder
                .HasOne(x => x.Project)
                .WithMany(x => x.Votes)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.Creator)
                .WithMany(x => x.ProjectsVotes)
                .HasForeignKey(x => x.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
