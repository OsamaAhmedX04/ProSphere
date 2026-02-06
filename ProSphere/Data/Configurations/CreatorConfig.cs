using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class CreatorConfig : IEntityTypeConfiguration<Creator>
    {
        public void Configure(EntityTypeBuilder<Creator> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.IsVerified).HasDefaultValue(false);

            builder
                .HasOne(c => c.User)
                .WithOne(a => a.Creator)
                .HasForeignKey<Creator>(c => c.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
