using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class ProjectUpdateImageHistoryConfig : IEntityTypeConfiguration<ProjectUpdateImageHistory>
    {
        public void Configure(EntityTypeBuilder<ProjectUpdateImageHistory> builder)
        {
            builder.HasKey(x => x.ProjectId);

            builder.Property(x => x.ImageUrl)
                .IsRequired();

            builder
                .HasOne(x => x.Project)
                .WithMany(x => x.ImagesHistory)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
