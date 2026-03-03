using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class ProjectUpdateHistoryConfig : IEntityTypeConfiguration<ProjectUpdateHistory>
    {
        public void Configure(EntityTypeBuilder<ProjectUpdateHistory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(e => e.Title)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.ShortDescription)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(e => e.Problem)
                .IsRequired();

            builder.Property(e => e.SolutionSummary)
                .IsRequired();

            builder.Property(e => e.Market)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.NeededInvestment)
                .HasPrecision(20, 2)
                .IsRequired();

            builder.Property(e => e.EquityPercentage)
                .HasPrecision(5, 2)
                .IsRequired();

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
                .WithOne(x => x.UpdatesHistory)
                .HasForeignKey<ProjectUpdateHistory>(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
