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
        public IRepository<IdentityVerification> IdentityVerifications { get; }
        public IRepository<ProfessionalVerification> ProfessionalVerifications { get; }
        public IRepository<Creator> Creators { get; }
        public IRepository<Investor> Investors { get; }

        public UnitOfWork(AppDbContext db)
        {
            _db = db;

            RefreshTokenAuths = new Repository<RefreshTokenAuth>(_db);

            ProfessionalVerifications = new Repository<ProfessionalVerification>(_db);
            FinancialVerifications = new Repository<FinancialVerification>(_db);
            IdentityVerifications = new Repository<IdentityVerification>(_db);

            Creators = new Repository<Creator>(_db);
            Investors = new Repository<Investor>(_db);


        }

        public async Task<int> CompleteAsync() => await _db.SaveChangesAsync();

        public void Dispose() => _db.Dispose();


    }
}
