using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class UserAccountHistoryConfig : IEntityTypeConfiguration<UserAccountHistory>
    {
        public void Configure(EntityTypeBuilder<UserAccountHistory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(v => v.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
