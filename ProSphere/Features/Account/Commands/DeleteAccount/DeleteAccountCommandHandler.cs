using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProSphere.Data.Context;
using ProSphere.Domain.Constants.TokenConstants;
using ProSphere.Domain.Entities;
using ProSphere.Jobs.Account.DeleteAccount;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Account.Commands.DeleteAccount
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _db;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAccountCommandHandler(UserManager<ApplicationUser> userManager, AppDbContext db, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _db = db;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteAccountCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.userId);


            if (user == null)
                return Result.Failure("User Not Found", StatusCodes.Status404NotFound);

            var otp = await _db.UserTokens
                .FirstOrDefaultAsync(t => t.Name == AuthenticationTokenInfo.DeleteAccountOTPName && t.UserId == user.Id && t.LoginProvider == AuthenticationTokenInfo.DefaultLoginProvider);

            if (otp == null || otp.Value != command.otp || otp.ExpireDate <= DateTime.UtcNow)
                return Result.Failure("OTP Is Invalid Or Expired", StatusCodes.Status404NotFound);


            await _userManager.RemoveAuthenticationTokenAsync(user, AuthenticationTokenInfo.DefaultLoginProvider, AuthenticationTokenInfo.DeleteAccountOTPName);

            var deleteUselessDataJob = BackgroundJob.Enqueue<IDeleteAccountJob>(
                service => service.DeleteUselessUserDataAsync(user)
                );

            BackgroundJob.ContinueJobWith<IDeleteAccountJob>(
                deleteUselessDataJob,
                service => service.MovePrivacyUserDataAsync(user)
                );

            BackgroundJob.Schedule<IDeleteAccountJob>(
                service => service.DeleteUserAsync(user.Id),
                TimeSpan.FromDays(30)
                );

            return Result.Success("User Deleted Successfully");

        }


    }
}
