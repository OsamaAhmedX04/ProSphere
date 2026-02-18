using FluentValidation;
using MediatR;
using ProSphere.Domain.Constants.FileConstants;
using ProSphere.Extensions;
using ProSphere.ExternalServices.Interfaces.FileStorage;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.CV.Commands.UploadCV
{
    public class UploadCVCommandHandler : IRequestHandler<UploadCVCommand, Result>
    {
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UploadCVRequest> _validator;

        public UploadCVCommandHandler(IFileService fileService, IUnitOfWork unitOfWork, IValidator<UploadCVRequest> validator)
        {
            _fileService = fileService;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result> Handle(UploadCVCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command.Request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }

            var user = await _unitOfWork.Creators.GetByIdAsync(command.UserId);
            if (user is null)
                return Result.Failure("User Not Found", StatusCodes.Status404NotFound);

            if (user.CVURL != null)
                await _fileService.DeleteAsync(SupabaseConstants.PrefixSupaURL + user.CVURL);

            var filePath = await _fileService.UploadAsync(command.Request.CV, "CVs");
            user.CVURL = filePath;
            await _unitOfWork.CompleteAsync();

            return Result.Success("CV Uploaded Successfully");
        }
    }
}
