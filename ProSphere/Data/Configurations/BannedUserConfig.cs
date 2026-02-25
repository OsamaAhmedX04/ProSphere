using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class BannedUserConfig : IEntityTypeConfiguration<BannedUser>
    {
        public void Configure(EntityTypeBuilder<BannedUser> builder)
        {
            builder.HasKey(x => x.UserId);

            builder.Property(x => x.Reason)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.BannedAt).HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.IsExpired).HasDefaultValue(false);

            builder
                .HasOne(x => x.User)
                .WithOne(x => x.BannedUser)
                .HasForeignKey<BannedUser>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
