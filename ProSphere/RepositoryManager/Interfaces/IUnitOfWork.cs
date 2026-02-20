using Microsoft.EntityFrameworkCore;
using ProSphere.Domain.Entities;
using ProSphere.RepositoryManager.Implementations;

namespace ProSphere.RepositoryManager.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<RefreshTokenAuth> RefreshTokenAuths { get; }

        IRepository<ProfessionalVerification> ProfessionalVerifications { get; }
        IRepository<ProfessionalVerificationHistory> ProfessionalVerificationHistories { get; }
        IRepository<IdentityVerification> IdentityVerifications { get; }
        IRepository<IdentityVerificationHistory> IdentityVerificationHistories { get; }
        IRepository<FinancialVerification> FinancialVerifications { get; }
        IRepository<FinancialVerificationHistory> FinancialVerificationHistories { get; }
        IRepository<UserAccountHistory> UserAccountHistories { get; }
        IRepository<FinancialDocumentType> FinancialDocumentTypes { get; }
        IRepository<ProfessionalDocumentType> ProfessionalDocumentTypes { get; }

        IRepository<Creator> Creators { get; }
        IRepository<Investor> Investors { get; }
        IRepository<Admin> Admins { get; }
        IRepository<Moderator> Moderators { get; }
        IRepository<Employee> Employees { get; }

        IRepository<Skill> Skills { get; }
        IRepository<CreatorSkill> CreatorSkills { get; }
        IRepository<UserSocialMedia> UsersSocialMedia { get; }

        IRepository<SearchHistory> SearchHistories { get; }


        IRepository<Project> Projects { get; }
        IRepository<ProjectDetail> ProjectsDetails { get; }
        IRepository<ProjectImage> ProjectsImages { get; }
        IRepository<ProjectAccessRequest> ProjectsAccessRequests { get; }
        IRepository<ProjectVote> ProjectsVotes { get; }
        IRepository<ProjectModeration> ProjectsModerations { get; }

        IRepository<ChatMessage> ChatMessages { get; }
        IRepository<ChatMessageHistory> ChatMessagesHistories { get; }
        IRepository<Notification> Notifications { get; }

        Task<int> CompleteAsync();
    }
}
