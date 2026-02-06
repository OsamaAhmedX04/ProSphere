using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProSphere.Data.Context;
using ProSphere.Domain.Constants;
using ProSphere.Domain.Entities;
using ProSphere.ResultResponse;

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
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();

            return Result.Success("User Deleted Successfully");

        }
    }
}
