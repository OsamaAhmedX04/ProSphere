using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using ProSphere.Domain.Constants.CacheConstants;
using ProSphere.Domain.Constants.RoleConstants;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.Features.Employee.Commands.AssignToModerator;
using ProSphere.Features.Moderator.Commands.RecycleModeratorAccount;
using ProSphere.Helpers.Generators;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;
using Supabase.Gotrue;

namespace ProSphere.Features.Employee.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<DeleteEmployeeCommandHandler> _logger;

        public DeleteEmployeeCommandHandler(IUnitOfWork unitOfWork, IMemoryCache cache, UserManager<ApplicationUser> userManager, ILogger<DeleteEmployeeCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.Employees.FirstOrDefaultAsync(e => e.Id == command.employeeId);
            if (employee is null)
                return Result.Failure("User Not Found", StatusCodes.Status404NotFound);

            var moderator = await _unitOfWork.Moderators.FirstOrDefaultAsync(m => m.Id == employee.AssignedTo && m.IsUsed);

            if (moderator is not null)
            {
                moderator.IsUsed = false;
                employee.AssignedTo = null;

                var moderatorAppUser = await _userManager.FindByIdAsync(moderator.Id);
                var roles = await _userManager.GetRolesAsync(moderatorAppUser!);
                var role = roles.FirstOrDefault();

                var tempPassword = PasswordGenerator.Generate(PasswordDificulty.High);

                await _userManager.RemovePasswordAsync(moderatorAppUser!);
                await _userManager.AddPasswordAsync(moderatorAppUser!, tempPassword);

                if (role == Role.Moderator)
                {
                    await _userManager.RemoveFromRoleAsync(moderatorAppUser!, Role.Moderator);
                    await _userManager.AddToRoleAsync(moderatorAppUser!, Role.InActiveModerator);
                }
                

                _cache.Remove(CacheKey.GetModeratorAvailableEmailsKey);
                _cache.Remove(CacheKey.GetModeratorAccountKey(moderator.Id));
            }

            employee.IsActive = false;
            employee.IsDeleted = true;
            employee.EndWorkAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();

            _logger.LogInformation("Employee with ID {EmployeeId} is deleted successfully", employee.Id);

            return Result.Success("Employee Is Deleted Successfully");
        }
    }
}
