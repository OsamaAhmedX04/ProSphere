using Microsoft.AspNetCore.Identity;
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
            builder.Property(au => au.Gender).IsRequired();


            builder.Property(au => au.Gender)
                .HasConversion<string>()
                .IsRequired();

            var admins = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = "d3b07384-d9f8-4f2a-8d3c-1b2a9e6f4f5e",
                    FirstName = "Super Admin",
                    LastName = "Prosphere",
                    Email = "adminProsphere@gmail.com",
                    NormalizedEmail = "ADMINPROSPHERE@GMAIL.COM",
                    IsVerified = true,
                    EmailConfirmed = true,
                    UserName = "adminProsphere@gmail.com",
                    NormalizedUserName = "ADMINPROSPHERE@GMAIL.COM",
                    PasswordHash = "AQAAAAIAAYagAAAAEF1P9msprFt8rq8JmHf0YuwxJQywTlpRnnLxK43Vx977UuiosltxCAfgW/bvmCyiFg==",
                    SecurityStamp = "11111111-1111-1111-1111-111111111111",
                    ConcurrencyStamp = "22222222-2222-2222-2222-222222222222"
                }
            };
            builder.HasData(admins);
        }
    }
}
