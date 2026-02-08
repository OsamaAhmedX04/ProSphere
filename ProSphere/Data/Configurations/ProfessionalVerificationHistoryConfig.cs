using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class ProfessionalVerificationHistoryConfig : IEntityTypeConfiguration<ProfessionalVerificationHistory>
    {
        public void Configure(EntityTypeBuilder<ProfessionalVerificationHistory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.InvestorEmail).IsUnique();
        }
    }
}
