using ProSphere.Domain.Entities;

namespace ProSphere.RepositoryManager.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<RefreshTokenAuth> RefreshTokenAuths { get; }

        IRepository<ProfessionalVerification> ProfessionalVerifications { get; }
        IRepository<IdentityVerification> IdentityVerifications { get; }
        IRepository<FinancialVerification> FinancialVerifications { get; }

        IRepository<Creator> Creators { get; }
        IRepository<Investor> Investors { get; }



        Task<int> CompleteAsync();
    }
}
