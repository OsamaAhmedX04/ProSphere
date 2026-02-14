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
        public IRepository<UserAccountHistory> UserAccountHistories { get; }
        public IRepository<Creator> Creators { get; }
        public IRepository<Investor> Investors { get; }
        public IRepository<Admin> Admins { get; }
        public IRepository<Moderator> Moderators { get; }
        public IRepository<Employee> Employees { get; }
        public IRepository<Skill> Skills { get; }
        public IRepository<CreatorSkill> CreatorSkills { get; }
        public IRepository<UserSocialMedia> UsersSocialMedia { get; }

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

            Creators = new Repository<Creator>(_db);
            Investors = new Repository<Investor>(_db);
            Admins = new Repository<Admin>(_db);
            Moderators = new Repository<Moderator>(_db);
            Employees = new Repository<Employee>(_db);

            Skills = new Repository<Skill>(_db);
            CreatorSkills = new Repository<CreatorSkill>(_db);
            UsersSocialMedia = new Repository<UserSocialMedia>(_db);


        }

        public async Task<int> CompleteAsync() => await _db.SaveChangesAsync();

        public void Dispose() => _db.Dispose();


    }
}
