using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class AdminConfig : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IsSuperAdmin).HasDefaultValue(false);

            builder
                .HasOne(c => c.User)
                .WithMany(a => a.Admins)
                .HasForeignKey(c => c.Id)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
