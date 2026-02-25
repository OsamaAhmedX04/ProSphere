using ProSphere.Data.Context;
using ProSphere.Domain.Entities;
using ProSphere.RepositoryManager.Interfaces;

namespace ProSphere.RepositoryManager.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        public IRepository<RefreshTokenAuth> RefreshTokenAuths { get; }
        public IRepository<FinancialVerification> FinancialVerifications { get; }
        public IRepository<FinancialVerificationHistory> FinancialVerificationHistories { get; }
        public IRepository<IdentityVerification> IdentityVerifications { get; }
        public IRepository<IdentityVerificationHistory> IdentityVerificationHistories { get; }
        public IRepository<ProfessionalVerification> ProfessionalVerifications { get; }
        public IRepository<ProfessionalVerificationHistory> ProfessionalVerificationHistories { get; }
        public IRepository<FinancialDocumentType> FinancialDocumentTypes { get; }
        public IRepository<ProfessionalDocumentType> ProfessionalDocumentTypes { get; }
        public IRepository<UserAccountHistory> UserAccountHistories { get; }
        public IRepository<Creator> Creators { get; }
        public IRepository<Investor> Investors { get; }
        public IRepository<Admin> Admins { get; }
        public IRepository<Moderator> Moderators { get; }
        public IRepository<Employee> Employees { get; }
        public IRepository<Skill> Skills { get; }
        public IRepository<CreatorSkill> CreatorSkills { get; }
        public IRepository<UserSocialMedia> UsersSocialMedia { get; }
        public IRepository<SearchHistory> SearchHistories { get; }

        public IRepository<Project> Projects { get; }
        public IRepository<ProjectDetail> ProjectsDetails { get; }
        public IRepository<ProjectAccessRequest> ProjectsAccessRequests { get; }
        public IRepository<ProjectImage> ProjectsImages { get; }
        public IRepository<ProjectModeration> ProjectsModerations { get; }
        public IRepository<ProjectVote> ProjectsVotes { get; }
        public IRepository<ProjectUpdateHistory> ProjectUpdatesHistories { get; }
        public IRepository<ProjectUpdateImageHistory> ProjectUpdatesImagesHistories { get; }

        public IRepository<ChatMessage> ChatMessages { get; }
        public IRepository<ChatMessageHistory> ChatMessagesHistories { get; }
        public IRepository<Notification> Notifications { get; }

        public IRepository<ReportedProject> ReportedProjects { get; }
        public IRepository<ReportedUser> ReportedUsers { get; }
        public IRepository<BannedUser> BannedUsers { get; }

        public UnitOfWork(AppDbContext db)
        {
            _db = db;

            RefreshTokenAuths = new Repository<RefreshTokenAuth>(_db);

            ProfessionalVerifications = new Repository<ProfessionalVerification>(_db);
            FinancialVerifications = new Repository<FinancialVerification>(_db);
            IdentityVerifications = new Repository<IdentityVerification>(_db);
            ProfessionalVerificationHistories = new Repository<ProfessionalVerificationHistory>(_db);
            FinancialVerificationHistories = new Repository<FinancialVerificationHistory>(_db);
            IdentityVerificationHistories = new Repository<IdentityVerificationHistory>(_db);
            UserAccountHistories = new Repository<UserAccountHistory>(_db);
            FinancialDocumentTypes = new Repository<FinancialDocumentType>(_db);
            ProfessionalDocumentTypes = new Repository<ProfessionalDocumentType>(_db);

            Creators = new Repository<Creator>(_db);
            Investors = new Repository<Investor>(_db);
            Admins = new Repository<Admin>(_db);
            Moderators = new Repository<Moderator>(_db);
            Employees = new Repository<Employee>(_db);

            Skills = new Repository<Skill>(_db);
            CreatorSkills = new Repository<CreatorSkill>(_db);
            UsersSocialMedia = new Repository<UserSocialMedia>(_db);

            SearchHistories = new Repository<SearchHistory>(_db);

            Projects = new Repository<Project>(_db);
            ProjectsDetails = new Repository<ProjectDetail>(_db);
            ProjectsAccessRequests = new Repository<ProjectAccessRequest>(_db);
            ProjectsModerations = new Repository<ProjectModeration>(_db);
            ProjectsVotes = new Repository<ProjectVote>(_db);
            ProjectsImages = new Repository<ProjectImage>(_db);
            ProjectUpdatesHistories = new Repository<ProjectUpdateHistory>(_db);
            ProjectUpdatesImagesHistories = new Repository<ProjectUpdateImageHistory>(_db);

            ChatMessages = new Repository<ChatMessage>(_db);
            ChatMessagesHistories = new Repository<ChatMessageHistory>(_db);
            Notifications = new Repository<Notification>(_db);

            ReportedProjects = new Repository<ReportedProject>(_db);
            ReportedUsers = new Repository<ReportedUser>(_db);
            BannedUsers = new Repository<BannedUser>(_db);

        }

        public async Task<int> CompleteAsync() => await _db.SaveChangesAsync();

        public void Dispose() => _db.Dispose();


    }
}
