using MediatR;
using ProSphere.Domain.Constants.FileConstants;
using ProSphere.ExternalServices.Interfaces.FileStorage;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectManagement.Commands.DeleteProject
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public DeleteProjectCommandHandler(IUnitOfWork unitOfWork, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public async Task<Result> Handle(DeleteProjectCommand command, CancellationToken cancellationToken)
        {
            var isCreatorExist = await _unitOfWork.Creators.IsExistAsync(command.creatorId);
            if (!isCreatorExist) return Result.Failure("Creator Not Found", StatusCodes.Status404NotFound);

            var project = await _unitOfWork.Projects.FirstOrDefaultAsync(p => p.Id == command.projectId);
            if (project is null) return Result.Failure("Project Not Found", StatusCodes.Status404NotFound);

            if (project.IsInvested)
                return Result.Failure("This Project Is Under Investing You Can NOT Delete this project", StatusCodes.Status400BadRequest);

            var projectImages = await _unitOfWork.ProjectsImages.GetAllAsyncEnhanced(p => p.ProjectId == command.projectId);
            if (!(projectImages is null || projectImages.Count == 0))
            {
                foreach (var image in projectImages)
                    await _fileService.DeleteAsync(SupabaseConstants.PrefixSupaURL + image.ImageUrl);

                await _unitOfWork.ProjectsImages.BulkDeleteAsync(p => p.ProjectId == command.projectId);
            }

            _unitOfWork.Projects.Delete(command.projectId);

            await _unitOfWork.CompleteAsync();

            return Result.Success("Project Deleted Successfully");
        }
    }
}
