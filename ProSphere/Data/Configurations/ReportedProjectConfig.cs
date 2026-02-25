using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class ReportedProjectConfig : IEntityTypeConfiguration<ReportedProject>
    {
        public void Configure(EntityTypeBuilder<ReportedProject> builder)
        {
            builder.HasKey(x => x.Id);


            builder.Property(x => x.Status).HasConversion<string>().IsRequired();
            builder.Property(x => x.Reason).HasConversion<string>().IsRequired();

            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            builder.Property(v => v.RowVersion)
                .IsRowVersion();

            builder
                .HasOne(x => x.Project)
                .WithMany(x => x.Reports)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.Reporter)
                .WithMany(x => x.ReportersOnProjects)
                .HasForeignKey(x => x.ReporterId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(x => x.Moderator)
                .WithMany(x => x.ReviewedProjectReports)
                .HasForeignKey(x => x.ReviewedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
