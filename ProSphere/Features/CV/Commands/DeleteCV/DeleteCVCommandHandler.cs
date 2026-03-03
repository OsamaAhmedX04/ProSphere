using MediatR;
using ProSphere.Domain.Constants.FileConstants;
using ProSphere.ExternalServices.Interfaces.FileStorage;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.CV.Commands.DeleteCV
{
    public class DeleteCVCommandHandler : IRequestHandler<DeleteCVCommand, Result>
    {
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCVCommandHandler(IFileService fileService, IUnitOfWork unitOfWork)
        {
            _fileService = fileService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteCVCommand command, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Creators.GetByIdAsync(command.UserId);
            if (user is null)
                return Result.Failure("User Not Found", StatusCodes.Status404NotFound);

            if (user.CVURL == null)
                return Result.Failure("No CV to delete", StatusCodes.Status400BadRequest);

            await _fileService.DeleteAsync(user.CVURL);
            user.CVURL = null;

            await _unitOfWork.CompleteAsync();
            return Result.Success("CV Deleted Successfully");
        }
    }
}
