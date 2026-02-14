using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class ProfessionalDocumentTypeConfig : IEntityTypeConfiguration<ProfessionalDocumentType>
    {
        public void Configure(EntityTypeBuilder<ProfessionalDocumentType> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

           builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.HasData(
                new ProfessionalDocumentType
                {
                    Id = 1,
                    Name = "Commercial Register",
                    Description = "Official commercial registration document."
                },
                new ProfessionalDocumentType
                {
                    Id = 2,
                    Name = "Tax Card",
                    Description = "Government-issued tax identification card."
                },
                new ProfessionalDocumentType
                {
                    Id = 3,
                    Name = "Membership",
                    Description = "Professional association membership proof."
                },
                new ProfessionalDocumentType
                {
                    Id = 4,
                    Name = "Letter",
                    Description = "Official professional verification letter."
                },
                new ProfessionalDocumentType
                {
                    Id = 5,
                    Name = "Other",
                    Description = "Any other professional proof document."
                }
                );
        }
    }
}
