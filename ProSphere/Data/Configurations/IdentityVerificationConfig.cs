using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class IdentityVerificationConfig : IEntityTypeConfiguration<IdentityVerification>
    {
        public void Configure(EntityTypeBuilder<IdentityVerification> builder)
        {
            builder.HasKey(v => v.Id);


            builder.Property(v => v.IdFrontImageURL).IsRequired();
            builder.Property(v => v.IdBackImageURL).IsRequired();
            builder.Property(v => v.SelfieWithIdURL).IsRequired();

            builder.Property(v => v.status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(v => v.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(v => v.RowVersion)
                .IsRowVersion();


            builder
                .HasOne(v => v.User)
                .WithMany(a => a.IdentityVerifications)
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(v => v.Moderator)
                .WithMany(a => a.ReviewedIdentityVerifications)
                .HasForeignKey(v => v.ReviewedBy)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
