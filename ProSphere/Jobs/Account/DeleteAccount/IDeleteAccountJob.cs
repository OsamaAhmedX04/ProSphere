using ProSphere.Domain.Entities;

namespace ProSphere.Jobs.Account.DeleteAccount
{
    public interface IDeleteAccountJob
    {
        Task DeleteUserAsync(string userId);
        Task DeleteUselessUserDataAsync(ApplicationUser user);
        Task MovePrivacyUserDataAsync(ApplicationUser user);
    }

}
