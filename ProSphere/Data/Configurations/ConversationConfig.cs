using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class ConversationConfig : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedAt)
                .IsRequired().HasDefaultValueSql("GETUTCDATE()");

            builder
                .HasOne(c => c.Creator)
                .WithMany(u => u.Conversations)
                .HasForeignKey(c => c.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(c => c.Investor)
                .WithMany(u => u.Conversations)
                .HasForeignKey(c => c.InvestorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
