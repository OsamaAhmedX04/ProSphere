using Microsoft.AspNetCore.Identity;
using ProSphere.Domain.Entities;
using ProSphere.RepositoryManager.Interfaces;

namespace ProSphere.Jobs.Ban.RemoveBan
{
    public class RemoveBanJob : IRemoveBanJob
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public RemoveBanJob(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task RemoveBan(string userId)
        {
            var banData = await _unitOfWork.BannedUsers.FirstOrDefaultAsync(x => x.UserId == userId);
            if (banData != null)
            {
                banData.IsExpired = true;
                var user = await _userManager.FindByIdAsync(banData.UserId);
                if (user != null)
                {
                    user.IsBanned = false;
                    await _userManager.UpdateAsync(user);
                }
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
