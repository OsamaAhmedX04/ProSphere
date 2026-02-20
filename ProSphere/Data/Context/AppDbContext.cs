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
        public DbSet<FinancialDocumentType> FinancialDocumentTypes { get; set; }
        public DbSet<ProfessionalVerification> ProfessionalVerifications { get; set; }
        public DbSet<ProfessionalDocumentType> ProfessionalDocumentTypes { get; set; }
        public DbSet<IdentityVerificationHistory> IdentityVerificationHistories { get; set; }
        public DbSet<FinancialVerificationHistory> FinancialVerificationHistories { get; set; }
        public DbSet<ProfessionalVerificationHistory> ProfessionalVerificationHistories { get; set; }
        public DbSet<UserAccountHistory> UserAccountHistories { get; set; }

        public DbSet<Creator> Creators { get; set; }
        public DbSet<Investor> Investors { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Moderator> Moderators { get; set; }
        public DbSet<Employee> Employees { get; set; }


        public DbSet<Skill> Skills { get; set; }
        public DbSet<CreatorSkill> CreatorSkills { get; set; }
        public DbSet<UserSocialMedia> UsersSocialMedia { get; set; }

        public DbSet<SearchHistory> SearchHistories { get; set; }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectDetail> ProjectsDetails { get; set; }
        public DbSet<ProjectImage> ProjectsImages { get; set; }
        public DbSet<ProjectAccessRequest> ProjectsAccessRequests { get; set; }
        public DbSet<ProjectVote> ProjectsVotes { get; set; }
        public DbSet<ProjectModeration> ProjectsModerations { get; set; }

        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<ChatMessageHistory> ChatMessagesHistories { get; set; }
        public DbSet<Notification> Notifications { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationUserConfig).Assembly);
        }
    }
}
