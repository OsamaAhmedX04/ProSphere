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

        }
    }
}
