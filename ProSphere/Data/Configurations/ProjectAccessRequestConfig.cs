using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;

namespace ProSphere.Data.Configurations
{
    public class ProjectAccessRequestConfig : IEntityTypeConfiguration<ProjectAccessRequest>
    {
        public void Configure(EntityTypeBuilder<ProjectAccessRequest> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.Message)
                .HasMaxLength(300);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");


            builder
                .HasOne(x => x.Investor)
                .WithMany(x => x.ProjectsAccessRequests)
                .HasForeignKey(x => x.InvestorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Creator)
                .WithMany(x => x.ProjectsAccessRequests)
                .HasForeignKey(x => x.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Project)
                .WithMany(x => x.AccessRequests)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
