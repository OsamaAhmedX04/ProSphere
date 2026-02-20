using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class ChatMessageHistoryConfig : IEntityTypeConfiguration<ChatMessageHistory>
    {
        public void Configure(EntityTypeBuilder<ChatMessageHistory> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(x => x.SentAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
