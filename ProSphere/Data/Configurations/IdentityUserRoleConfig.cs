using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProSphere.Data.Configurations
{
    public class IdentityUserRoleConfig : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            var adminsRole = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    UserId = "d3b07384-d9f8-4f2a-8d3c-1b2a9e6f4f5e",
                    RoleId = "c1f8e7d4-5a2b-4c6d-9f1e-3b7a8d2c9f4e"
                }
            };

            builder.HasData(adminsRole);
        }
    }
}
