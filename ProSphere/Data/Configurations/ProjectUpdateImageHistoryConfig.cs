using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class ProjectUpdateImageHistoryConfig : IEntityTypeConfiguration<ProjectUpdateImageHistory>
    {
        public void Configure(EntityTypeBuilder<ProjectUpdateImageHistory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ImageUrl)
                .IsRequired();

            builder
                .HasOne(x => x.ProjectUpdateHistory)
                .WithMany(x => x.ImagesHistory)
                .HasForeignKey(x => x.ProjectUpdateHistoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
