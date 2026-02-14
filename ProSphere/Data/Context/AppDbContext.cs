using Microsoft.EntityFrameworkCore;
using ProSphere.Data.Configurations;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Context
{
    public class AppDbContext : CustomIdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<RefreshTokenAuth> RefreshTokens { get; set; }

        public DbSet<IdentityVerification> IdentityVerifications { get; set; }
        public DbSet<FinancialVerification> FinancialVerifications { get; set; }
        public DbSet<ProfessionalVerification> ProfessionalVerifications { get; set; }
        public DbSet<IdentityVerificationHistory> IdentityVerificationHistories { get; set; }
        public DbSet<FinancialVerificationHistory> FinancialVerificationHistories { get; set; }
        public DbSet<ProfessionalVerificationHistory> ProfessionalVerificationHistories { get; set; }
        public DbSet<UserAccountHistory> UserAccountHistories { get; set; }

        public DbSet<Creator> Creators { get; set; }
        public DbSet<Investor> Investors { get; set; }
        public DbSet<Admin> Amdins { get; set; }
        public DbSet<Moderator> Moderators { get; set; }
        public DbSet<Employee> Employees { get; set; }


        public DbSet<Skill> Skills { get; set; }
        public DbSet<CreatorSkill> CreatorSkills { get; set; }
        public DbSet<UserSocialMedia> UsersSocialMedia { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationUserConfig).Assembly);
        }
    }
}
