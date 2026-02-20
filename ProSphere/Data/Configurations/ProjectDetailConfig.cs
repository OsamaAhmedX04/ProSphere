using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class ProjectDetailConfig : IEntityTypeConfiguration<ProjectDetail>
    {
        public void Configure(EntityTypeBuilder<ProjectDetail> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ExecutionPlan)
                .IsRequired();

            builder.Property(x => x.FinancialDetails)
                .IsRequired();

            builder.Property(x => x.BusinessModel)
                .IsRequired();

            builder.Property(x => x.MarketingStrategy)
                .IsRequired();

            builder
                .HasOne(x => x.Project)
                .WithOne(x => x.Details)
                .HasForeignKey<ProjectDetail>(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
