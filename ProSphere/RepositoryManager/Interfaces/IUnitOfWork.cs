using ProSphere.Domain.Entities;

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

        IRepository<Creator> Creators { get; }
        IRepository<Investor> Investors { get; }



        Task<int> CompleteAsync();
    }
}
