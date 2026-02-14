using FluentValidation;
using MediatR;
using ProSphere.Domain.Constants;
using ProSphere.Extensions;
using ProSphere.ExternalServices.Interfaces.FileStorage;
using ProSphere.Features.Account.Commands.UpdateCreatorAccount;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProSphere.Features.Account.Commands.UpdateInvestorAccount
{
    public class UpdateInvestorAccountCommandHandler : IRequestHandler<UpdateInvestorAccountCommand, Result>
    {
        private readonly IValidator<UpdateInvestorAccountRequest> _validator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public UpdateInvestorAccountCommandHandler(IValidator<UpdateInvestorAccountRequest> validator,
            IUnitOfWork unitOfWork, IFileService fileService)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
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

            if (command.request.ImageProfile != null)
            {
                if (investor.ImageProfileURL != null)
                    await _fileService.DeleteAsync(SupabaseConstants.PrefixSupaURL + investor.ImageProfileURL);

                var imagePath = await _fileService.UploadAsync(command.request.ImageProfile, "Investors/ProfileImages");

                investor.ImageProfileURL = imagePath;
            }

            investor.BIO = command.request.BIO;
            investor.HeadLine = command.request.HeadLine;

            await _unitOfWork.CompleteAsync();

            return Result.Success("Investor Account Updated Successfully");
        }
    }
}
