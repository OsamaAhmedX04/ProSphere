using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProSphere.Data.Context;
using ProSphere.Domain.Constants;
using ProSphere.Domain.Entities;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;
using Supabase.Gotrue;

namespace ProSphere.Features.Account.Commands.DeleteAccount
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _db;

        public DeleteAccountCommandHandler(UserManager<ApplicationUser> userManager, AppDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<Result> Handle(DeleteAccountCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.userId);

            if (user == null)
                return Result.Failure("User Not Found", StatusCodes.Status404NotFound);

            var otp = await _db.UserTokens
                .FirstOrDefaultAsync(t => t.Name == TokenInfo.DeleteAccountOTPName && t.UserId == user.Id && t.LoginProvider == TokenInfo.DefaultLoginProvider);

            if (otp == null || otp.Value != command.otp || otp.ExpireDate <= DateTime.UtcNow)
                return Result.Failure("OTP Is Invalid Or Expired", StatusCodes.Status404NotFound);

            
            await _userManager.RemoveAuthenticationTokenAsync(user, TokenInfo.DefaultLoginProvider, TokenInfo.DeleteAccountOTPName);
            
            await DeleteUser(user);

            return Result.Success("User Deleted Successfully");

        }

        private async Task DeleteUser(ApplicationUser user)
        {
            await DeleteUserPrivacyData(user);

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }
        private async Task DeleteUserPrivacyData(ApplicationUser user )
        {
            if (await _userManager.IsInRoleAsync(user, Role.Investor) || await _userManager.IsInRoleAsync(user, Role.Creator))
                await _db.IdentityVerifications.Where(x => x.UserId == user.Id).ExecuteDeleteAsync();

            if (await _userManager.IsInRoleAsync(user, Role.Investor))
            {
                await _db.FinancialVerifications.Where(x => x.InvestorId == user.Id).ExecuteDeleteAsync();
                await _db.ProfessionalVerifications.Where(x => x.InvestorId == user.Id).ExecuteDeleteAsync();
            }
        }
    }
}
