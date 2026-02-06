using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class ProfessionalVerificationConfig : IEntityTypeConfiguration<ProfessionalVerification>
    {
        public void Configure(EntityTypeBuilder<ProfessionalVerification> builder)
        {
            builder.HasKey(v => v.Id);


            builder.Property(v => v.DocumentURL).IsRequired();

            builder.Property(v => v.DocumentType)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(v => v.status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(v => v.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder
                .HasOne(v => v.Investor)
                .WithMany(i => i.ProfessionalVerifications)
                .HasForeignKey(v => v.InvestorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(v => v.Moderator)
                .WithMany(a => a.ReviewedProfessionalVerifications)
                .HasForeignKey(v => v.ReviewedBy)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
