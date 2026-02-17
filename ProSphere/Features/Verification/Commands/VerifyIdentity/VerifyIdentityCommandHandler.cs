using FluentValidation;
using MediatR;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.Extensions;
using ProSphere.ExternalServices.Interfaces.FileStorage;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Verification.Commands.VerifyIdentity
{
    public class VerifyIdentityCommandHandler : IRequestHandler<VerifyIdentityCommand, Result>
    {
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<VerifyIdentityRequest> _validator;

        public VerifyIdentityCommandHandler(IFileService fileService, IUnitOfWork unitOfWork, IValidator<VerifyIdentityRequest> validator)
        {
            _fileService = fileService;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result> Handle(VerifyIdentityCommand command, CancellationToken cancellationToken)
        {
            var isTherePendingDocs = await _unitOfWork.IdentityVerifications
                .FirstOrDefaultAsync(v => v.UserId == command.userId && v.status == Status.Pending) != null;
            if (isTherePendingDocs)
                return Result.Failure("You Already Have Uploaded You Identity Verification And Its Under Revision", 400);

            var validationResult = await _validator.ValidateAsync(command.request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }

            var idFrontImageURL = await _fileService.UploadAsync(command.request.IdFrontImage, $"Verifications/Identities/{command.userId}");
            var idBackImageURL = await _fileService.UploadAsync(command.request.IdBackImage, $"Verifications/Identities/{command.userId}");
            var selfieWithIdImageURL = await _fileService.UploadAsync(command.request.SelfieWithId, $"Verifications/Identities/{command.userId}");

            var identityVerification = new IdentityVerification
            {
                UserId = command.userId,
                IdFrontImageURL = idFrontImageURL,
                IdBackImageURL = idBackImageURL,
                SelfieWithIdURL = selfieWithIdImageURL,
                status = Status.Pending
            };
            await _unitOfWork.IdentityVerifications.AddAsync(identityVerification);
            await _unitOfWork.CompleteAsync();

            return Result.Success("Identity Verification Documents Have Been Sent Successfully And It's Now Under Revision");
        }
    }
}
