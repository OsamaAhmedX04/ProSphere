using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class FinancialDocumentTypeConfig : IEntityTypeConfiguration<FinancialDocumentType>
    {
        public void Configure(EntityTypeBuilder<FinancialDocumentType> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(f => f.Description)
                 .HasMaxLength(500);

            builder.HasData(
                new FinancialDocumentType
                {
                    Id = 1,
                    Name = "Bank Statement",
                    Description = "Official bank account statement issued by a financial institution."
                },
                new FinancialDocumentType
                {
                    Id = 2,
                    Name = "Wallet",
                    Description = "Digital wallet statement or proof of funds."
                },
                new FinancialDocumentType
                {
                    Id = 3,
                    Name = "Financial Letter",
                    Description = "Certified financial capability letter."
                },
                new FinancialDocumentType
                {
                    Id = 4,
                    Name = "Other",
                    Description = "Any other financial proof document."
                }
                );
        }
    }
}
