using MediatR;
using Microsoft.EntityFrameworkCore;
using ProSphere.Domain.Constants.FileConstants;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.ExternalServices.Interfaces.FileStorage;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectModerating.Commands.AcceptProjectCreation
{
    public class AcceptProjectCreationCommandHandler : IRequestHandler<AcceptProjectCreationCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public AcceptProjectCreationCommandHandler(IUnitOfWork unitOfWork, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public async Task<Result> Handle(AcceptProjectCreationCommand command, CancellationToken cancellationToken)
        {
            var isCreatorExist = await _unitOfWork.Moderators.IsExistAsync(command.moderatorId);
            if (!isCreatorExist) return Result.Failure("Moderator Not Found", StatusCodes.Status404NotFound);

            var project = await _unitOfWork.Projects.FirstOrDefaultAsync(p => p.Id == command.projectId && p.Status == Status.Pending);
            if (project is null) return Result.Failure("Project Not Found", StatusCodes.Status404NotFound);

            


            // Updated Project
            if (project.UpdatedAt.HasValue)
            {
                var updatedProject = await _unitOfWork.ProjectUpdatesHistories.FirstOrDefaultAsync(p => p.ProjectId == command.projectId);
                if (updatedProject is null || updatedProject.Status != Status.Pending)
                    return Result.Failure("Updated Project Not Found", StatusCodes.Status404NotFound);

                project.Title = updatedProject.Title;
                project.ShortDescription = updatedProject.ShortDescription;
                project.Problem = updatedProject.Problem;
                project.SolutionSummary = updatedProject.SolutionSummary;
                project.Market = updatedProject.Market;
                project.NeededInvestment = updatedProject.NeededInvestment;
                project.EquityPercentage = updatedProject.EquityPercentage;
                project.IsActive = true;
                project.Status = Status.Approved;

                var projectDetails = await _unitOfWork.ProjectsDetails.FirstOrDefaultAsync(p => p.ProjectId == command.projectId);

                if (projectDetails != null)
                {
                    projectDetails.ExecutionPlan = updatedProject.ExecutionPlan;
                    projectDetails.FinancialDetails = updatedProject.FinancialDetails;
                    projectDetails.BusinessModel = updatedProject.BusinessModel;
                    projectDetails.MarketingStrategy = updatedProject.MarketingStrategy;
                    projectDetails.Notes = updatedProject.Notes;
                }

                var oldImagesurl = await _unitOfWork.ProjectsImages.GetAllAsyncEnhanced(
                    filter: p => p.ProjectId == project.Id,
                    selector: p => p.ImageUrl
                    );

                foreach(var imageURL in oldImagesurl)
                {
                    await _fileService.DeleteAsync(SupabaseConstants.PrefixSupaURL + imageURL);
                }
                await _unitOfWork.ProjectsImages.BulkDeleteAsync(p => p.ProjectId == command.projectId);




                var newImagesURL = await _unitOfWork.ProjectUpdatesImagesHistories.GetAllAsyncEnhanced(
                    filter: p => p.ProjectId == project.Id,
                    selector: p => p.ImageUrl
                    );

                if (newImagesURL != null)
                {
                    var projectImages = new List<ProjectImage>();
                    foreach (var imageURL in newImagesURL)
                    {
                        projectImages.Add(new ProjectImage { ImageUrl = imageURL, ProjectId = command.projectId });
                    }
                    await _unitOfWork.ProjectsImages.AddRangeAsync(projectImages);
                }


                _unitOfWork.ProjectUpdatesHistories.Delete(updatedProject.ProjectId);
            }

            var moderationResult = new ProjectModeration
            {
                ModeratorId = command.moderatorId,
                ProjectId = command.projectId,
                Status = Status.Approved,
                IsUpdate = project.UpdatedAt.HasValue,
            };
            await _unitOfWork.ProjectsModerations.AddAsync(moderationResult);
            project.Status = Status.Approved;
            project.IsActive = true;

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Result.Failure("Error: Another Moderator Took An Action", StatusCodes.Status409Conflict);
            }

            return Result.Success("Project Is Approved Successfully");
        }
    }
}
