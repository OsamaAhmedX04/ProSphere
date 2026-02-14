using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class FinancialVerificationHistoryConfig : IEntityTypeConfiguration<FinancialVerificationHistory>
    {
        public void Configure(EntityTypeBuilder<FinancialVerificationHistory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.InvestorEmail).IsUnique();

            builder.HasOne(x => x.DocumentType)
                .WithMany(t => t.FinancialVerificationHistories)
                .HasForeignKey(x => x.DocumentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
