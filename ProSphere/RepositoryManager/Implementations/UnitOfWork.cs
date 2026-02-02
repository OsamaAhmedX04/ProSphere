using ProSphere.Data.Context;
using ProSphere.Domain.Entities;
using ProSphere.RepositoryManager.Interfaces;

namespace ProSphere.RepositoryManager.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        public IRepository<RefreshTokenAuth> RefreshTokenAuths { get; }

        public UnitOfWork(AppDbContext db)
        {
            _db = db;

            RefreshTokenAuths = new Repository<RefreshTokenAuth>(_db);

        }

        public async Task<int> CompleteAsync() => await _db.SaveChangesAsync();

        public void Dispose() => _db.Dispose();


    }
}
