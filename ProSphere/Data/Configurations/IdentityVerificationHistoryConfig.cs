using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class IdentityVerificationHistoryConfig : IEntityTypeConfiguration<IdentityVerificationHistory>
    {
        public void Configure(EntityTypeBuilder<IdentityVerificationHistory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.UserEmail).IsUnique();
        }
    }
}
