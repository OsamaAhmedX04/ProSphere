using ProSphere.Domain.Entities;
using System.Reactive;

namespace ProSphere.RepositoryManager.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<RefreshTokenAuth> RefreshTokenAuths { get; }

        

        Task<int> CompleteAsync();
    }
}
