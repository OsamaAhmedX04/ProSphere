using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using ProSphere.Domain.Constants.CacheConstants;
using ProSphere.Domain.Constants.RoleConstants;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.Helpers.Generators;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Moderator.Commands.RecycleModeratorAccount
{
    public class RecycleModeratorAccountCommandHandler : IRequestHandler<RecycleModeratorAccountCommand, Result<RecycleModeratorAccountResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public RecycleModeratorAccountCommandHandler(UserManager<ApplicationUser> userManager, IMemoryCache cache, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _cache = cache;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<RecycleModeratorAccountResponse>> Handle(RecycleModeratorAccountCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.moderatorId);

            if (user == null)
                return Result<RecycleModeratorAccountResponse>.Failure("User Not Found", StatusCodes.Status404NotFound);

            var userRoles = await _userManager.GetRolesAsync(user);
            if (!(userRoles.Contains(Role.Moderator) || userRoles.Contains(Role.InActiveModerator)))
                return Result<RecycleModeratorAccountResponse>.Failure("User Not Found", StatusCodes.Status404NotFound);

            var response = new RecycleModeratorAccountResponse { TempPassword = PasswordGenerator.Generate(PasswordDificulty.Low) };

            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, response.TempPassword);

            await _userManager.RemoveFromRoleAsync(user, Role.Moderator);

            if (!userRoles.Contains(Role.InActiveModerator))
                await _userManager.AddToRoleAsync(user, Role.InActiveModerator);


            var moderator = await _unitOfWork.Moderators.FirstOrDefaultAsync(m => m.Id == command.moderatorId);
            if (moderator.IsUsed)
            {
                var employee = await _unitOfWork.Employees.FirstOrDefaultAsync(e => e.AssignedTo == moderator.Id && e.IsActive);
                employee!.IsActive = false;
                employee!.EndWorkAt = DateTime.UtcNow;
                employee!.AssignedTo = null;
                moderator.IsUsed = false;

                await _unitOfWork.CompleteAsync();
            }




            _cache.Remove(CacheKey.GetModeratorAccountKey(command.moderatorId));
            _cache.Remove(CacheKey.GetModeratorAvailableEmailsKey);


            return Result<RecycleModeratorAccountResponse>.Success(response, "Moderator Account Recycled Successfully");
        }
    }
}
