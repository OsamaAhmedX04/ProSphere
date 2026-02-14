using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(e => e.Email)
                .IsRequired();

            builder.Property(e => e.Phone)
                .IsRequired();

            builder.Property(e => e.Country)
                .IsRequired();

            builder.Property(e => e.IsDeleted).HasDefaultValue(false);

            builder.Property(e => e.IsActive).HasDefaultValue(true);

            builder.Property(e => e.StartWorkAt).HasDefaultValueSql("GETUTCDATE()");

            builder
                .HasQueryFilter(e => !e.IsDeleted);

            builder
                .HasOne(e => e.Moderator)
                .WithMany(m => m.Employees)
                .HasForeignKey(e => e.AssignedTo)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
