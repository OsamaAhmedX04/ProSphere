using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class ModeratorConfig : IEntityTypeConfiguration<Moderator>
    {
        public void Configure(EntityTypeBuilder<Moderator> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .IsRequired();

            builder.Property(x => x.IsUsed).HasDefaultValue(false);

            builder
                .HasOne(c => c.User)
                .WithMany(a => a.Moderators)
                .HasForeignKey(c => c.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
