using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class RefreshTokenConfig : IEntityTypeConfiguration<RefreshTokenAuth>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenAuth> builder)
        {
            builder.HasKey(rf => rf.UserId);
        }
    }
}
