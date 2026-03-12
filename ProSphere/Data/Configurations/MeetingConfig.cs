using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class MeetingConfig : IEntityTypeConfiguration<Meeting>
    {
        public void Configure(EntityTypeBuilder<Meeting> builder)
        {
            builder.HasKey(e => e.Id);


            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");


            builder
                .HasOne(e => e.Creator)
                .WithMany(c => c.Meetings)
                .HasForeignKey(e => e.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(e => e.Investor)
                .WithMany(i => i.Meetings)
                .HasForeignKey(e => e.InvestorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
