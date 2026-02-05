using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(au => au.FirstName).HasMaxLength(40).IsRequired();
            builder.Property(au => au.LastName).HasMaxLength(40).IsRequired();
            builder.Property(au => au.IsActive).HasDefaultValue(false);

            builder.Property(au => au.Gender)
                .HasConversion<string>()
                .IsRequired();
        }
    }
}
