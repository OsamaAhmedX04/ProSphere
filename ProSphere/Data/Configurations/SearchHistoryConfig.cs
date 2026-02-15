using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class SearchHistoryConfig : IEntityTypeConfiguration<SearchHistory>
    {
        public void Configure(EntityTypeBuilder<SearchHistory> builder)
        {
            builder.HasKey(sh => sh.Id);

            builder.Property(sh => sh.SearchTerm)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(sh => sh.SearchCategory)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(sh => sh.SearchDate).HasDefaultValueSql("GETUTCDATE()");

            builder.HasIndex(e => new { e.UserId, e.SearchDate })
              .HasDatabaseName("IX_SearchHistories_UserId_SearchDate");

            builder.HasOne(sh => sh.User)
                    .WithMany(u => u.SearchHistories)
                    .HasForeignKey(sh => sh.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
