using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class InvestorConfig : IEntityTypeConfiguration<Investor>
    {
        public void Configure(EntityTypeBuilder<Investor> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.InvestorLevel)
                .HasConversion<string>()
                .IsRequired();
            

            builder
                .HasOne(i => i.User)
                .WithOne(a => a.Investor)
                .HasForeignKey<Investor>(i => i.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
