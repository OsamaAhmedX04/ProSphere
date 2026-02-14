using FluentValidation;
using MediatR;
using ProSphere.Domain.Constants;
using ProSphere.Extensions;
using ProSphere.ExternalServices.Interfaces.FileStorage;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Account.Commands.UpdateCreatorAccount
{
    public class UpdateCreatorAccountCommandHandler : IRequestHandler<UpdateCreatorAccountCommand, Result>
    {
        private readonly IValidator<UpdateCreatorAccountRequest> _validator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public UpdateCreatorAccountCommandHandler(IValidator<UpdateCreatorAccountRequest> validator,
            IUnitOfWork unitOfWork, IFileService fileService)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public async Task<Result> Handle(UpdateCreatorAccountCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command.request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }

            var creator = await _unitOfWork.Creators.FirstOrDefaultAsync(c => c.Id == command.creatorId);
            if(creator == null)
                return Result.Failure("Creator Not Found", StatusCodes.Status404NotFound);

            if (command.request.ImageProfile != null)
            {
                if(creator.ImageProfileURL != null)
                    await _fileService.DeleteAsync(SupabaseConstants.PrefixSupaURL + creator.ImageProfileURL);

                var imagePath = await _fileService.UploadAsync(command.request.ImageProfile, "Creators/ProfileImages");

                creator.ImageProfileURL = imagePath;
            }

            creator.BIO = command.request.BIO;
            creator.HeadLine = command.request.HeadLine;

            await _unitOfWork.CompleteAsync();

            return Result.Success("Creator Account Updated Successfully");

        }
    }
}
