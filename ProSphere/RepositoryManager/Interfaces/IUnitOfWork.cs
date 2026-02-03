using ProSphere.Domain.Entities;

namespace ProSphere.RepositoryManager.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<RefreshTokenAuth> RefreshTokenAuths { get; }



        Task<int> CompleteAsync();
    }
}
