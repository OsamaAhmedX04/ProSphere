using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProSphere.Data.Context;
using ProSphere.Domain.Constants;
using ProSphere.Domain.Entities;
using ProSphere.Extensions;
using ProSphere.ExternalServices.Interfaces.Email;
using ProSphere.Helpers;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Account.Commands.RequestDeleteAccount
{
    public class RequestDeleteAccountCommandHandler : IRequestHandler<RequestDeleteAccountCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSenderService _emailSenderService;
        private readonly AppDbContext _db;

        public RequestDeleteAccountCommandHandler(UserManager<ApplicationUser> userManager, IEmailSenderService emailSenderService, AppDbContext db)
        {
            _userManager = userManager;
            _emailSenderService = emailSenderService;
            _db = db;
        }

        public async Task<Result> Handle(RequestDeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.userId);

            if (user == null)
                return Result.Failure("User Not Found", StatusCodes.Status404NotFound);

            //if (user.IsDeleted)
            //    return Result.Failure("User Not Found", StatusCodes.Status404NotFound);

            var otp = OTPGenerator.Generate();

            var result = await _userManager.SetAuthenticationTokenAsync(user, AuthenticationTokenInfo.DefaultLoginProvider, AuthenticationTokenInfo.DeleteAccountOTPName, otp);

            if (!result.Succeeded)
            {
                var errors = result.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }

            var token = await _db.UserTokens.FirstOrDefaultAsync(t => t.Name == AuthenticationTokenInfo.DeleteAccountOTPName && t.UserId == user.Id);

            token!.ExpireDate = DateTime.UtcNow.AddMinutes(15);

            await _db.SaveChangesAsync();

            _emailSenderService.SendDeleteAccountOTPMail(user.Email!, otp);

            return Result.Success("We Have Sent OTP In Your Mail");
        }
    }
}
