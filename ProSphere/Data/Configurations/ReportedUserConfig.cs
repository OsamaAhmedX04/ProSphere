using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class ReportedUserConfig : IEntityTypeConfiguration<ReportedUser>
    {
        public void Configure(EntityTypeBuilder<ReportedUser> builder)
        {
            builder.HasKey(x => x.Id);


            builder.Property(x => x.Status).HasConversion<string>().IsRequired();
            builder.Property(x => x.Reason).HasConversion<string>().IsRequired();

            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            builder.Property(v => v.RowVersion)
                .IsRowVersion();

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.ReportedUsers)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.Reporter)
                .WithMany(x => x.ReportersOnUsers)
                .HasForeignKey(x => x.ReporterId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(x => x.Moderator)
                .WithMany(x => x.ReviewedUserReports)
                .HasForeignKey(x => x.ReviewedBy)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
