using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using ProSphere.Domain.Constants.CacheConstants;
using ProSphere.Domain.Entities;
using ProSphere.Extensions;
using ProSphere.ExternalServices.Interfaces.FileStorage;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Account.Commands.UpdateInvestorAccount
{
    public class UpdateInvestorAccountCommandHandler : IRequestHandler<UpdateInvestorAccountCommand, Result>
    {
        private readonly IValidator<UpdateInvestorAccountRequest> _validator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly IMemoryCache _cache;

        public UpdateInvestorAccountCommandHandler(IValidator<UpdateInvestorAccountRequest> validator,
            IUnitOfWork unitOfWork, IFileService fileService, UserManager<ApplicationUser> userManager, IMemoryCache cache)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _userManager = userManager;
            _cache = cache;
        }

        public async Task<Result> Handle(UpdateInvestorAccountCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command.request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }

            var investor = await _unitOfWork.Investors.FirstOrDefaultAsync(c => c.Id == command.investorId);
            if (investor == null)
                return Result.Failure("Investor Not Found", StatusCodes.Status404NotFound);

            var user = await _userManager.FindByIdAsync(investor.Id);

            user!.FirstName = command.request.FirstName;
            user.LastName = command.request.LastName;
            var settingResult = await _userManager.SetUserNameAsync(user, command.request.Username);
            if (!settingResult.Succeeded)
            {
                var errors = settingResult.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }

            await _userManager.UpdateAsync(user);

            if (command.request.ImageProfile != null)
            {
                if (investor.ImageProfileURL != null)
                    await _fileService.DeleteAsync(investor.ImageProfileURL);
                //await _fileService.DeleteAsync(SupabaseConstants.PrefixSupaURL + investor.ImageProfileURL);

                var imagePath = await _fileService.UploadAsync(command.request.ImageProfile, "Investors/ProfileImages");

                investor.ImageProfileURL = imagePath;
            }

            investor.FullName = command.request.FirstName + " " + command.request.LastName;
            investor.BIO = command.request.BIO;
            investor.HeadLine = command.request.HeadLine;

            await _unitOfWork.CompleteAsync();

            _cache.Remove(CacheKey.GetInvestorAccountKey(user.UserName!));

            return Result.Success("Investor Account Updated Successfully");
        }
    }
}
