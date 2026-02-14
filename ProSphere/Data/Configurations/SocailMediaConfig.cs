using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class SocailMediaConfig : IEntityTypeConfiguration<UserSocialMedia>
    {
        public void Configure(EntityTypeBuilder<UserSocialMedia> builder)
        {
            builder.HasKey(usm => usm.Id);


            builder
                .HasOne(usm => usm.User)
                .WithOne(u => u.SocialMedia)
                .HasForeignKey<UserSocialMedia>(usm => usm.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
