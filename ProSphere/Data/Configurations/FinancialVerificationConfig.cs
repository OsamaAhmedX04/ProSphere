using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class FinancialVerificationConfig : IEntityTypeConfiguration<FinancialVerification>
    {
        public void Configure(EntityTypeBuilder<FinancialVerification> builder)
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
                .WithOne(i => i.FinancialVerification)
                .HasForeignKey<FinancialVerification>(v => v.InvestorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(v => v.Moderator)
                .WithMany(a => a.ReviewedFinancialVerifications)
                .HasForeignKey(v => v.ReviewedBy)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
