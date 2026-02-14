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
