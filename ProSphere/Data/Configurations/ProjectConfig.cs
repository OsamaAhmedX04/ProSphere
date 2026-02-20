using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class ProjectConfig : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(e => e.Id);

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
                .HasPrecision(20,2)
                .IsRequired();

            builder.Property(e => e.EquityPercentage)
                .HasPrecision(5, 2)
                .IsRequired();

            builder.Property(e => e.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.IsActive)
                .HasDefaultValue(false);


            builder
                .HasOne(e => e.Creator)
                .WithMany(e => e.Projects)
                .HasForeignKey(e => e.CreatorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
