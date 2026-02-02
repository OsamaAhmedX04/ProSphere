using Microsoft.EntityFrameworkCore;
using ProSphere.Data.Configurations;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Context
{
    public class AppDbContext : CustomIdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<RefreshTokenAuth> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationUserConfig).Assembly);
        }
    }
}
