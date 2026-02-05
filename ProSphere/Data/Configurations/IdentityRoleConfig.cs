using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProSphere.Data.Configurations
{
    public class IdentityRoleConfig : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "9f8d4b9b-1e52-4dbf-a356-2c62b9db5b01",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "11111111-1111-1111-1111-111111111111"
                },
                new IdentityRole
                {
                    Id = "a7e13e2a-63a7-4c5d-a5b9-5b49efb0123f",
                    Name = "Creator",
                    NormalizedName = "CREATOR",
                    ConcurrencyStamp = "22222222-2222-2222-2222-222222222222"
                },
                new IdentityRole
                {
                    Id = "c3e4c1a9-4f7e-4a42-9c36-8d2a6e9c75bd",
                    Name = "Investor",
                    NormalizedName = "INVESTOR",
                    ConcurrencyStamp = "22222222-2222-2200-0000-000000000000"
                },
                new IdentityRole
                {
                    Id = "3f209613-7a40-4a9b-b270-ff3a39a1337c",
                    Name = "Moderator",
                    NormalizedName = "MODERATOR",
                    ConcurrencyStamp = "33333333-3333-3300-0000-000000000000"
                },
                new IdentityRole
                {
                    Id = "b3f14a23-34a5-4bc0-912d-0f2f1d8d4a11",
                    Name = "Expert",
                    NormalizedName = "EXPERT",
                    ConcurrencyStamp = "33333333-3333-3333-3333-333333333333"
                },
            };

            builder.HasData(roles);
        }
    }
}
