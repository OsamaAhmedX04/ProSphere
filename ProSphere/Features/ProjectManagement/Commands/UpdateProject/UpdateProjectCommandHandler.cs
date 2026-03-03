using FluentValidation;
using MediatR;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.Extensions;
using ProSphere.ExternalServices.Interfaces.FileStorage;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectManagement.Commands.UpdateProject
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateProjectRequest> _validator;
        private readonly IFileService _fileService;

        public UpdateProjectCommandHandler(IUnitOfWork unitOfWork, IValidator<UpdateProjectRequest> validator, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _fileService = fileService;
        }

        public async Task<Result> Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command.request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }

            var isCreatorExist = await _unitOfWork.Creators.IsExistAsync(command.creatorId);
            if (!isCreatorExist) return Result.Failure("Creator Not Found", StatusCodes.Status404NotFound);

            var project = await _unitOfWork.Projects.FirstOrDefaultAsync(p => p.Id == command.projectId);
            if (project is null) return Result.Failure("Project Not Found", StatusCodes.Status404NotFound);

            if (project.Status == Status.Pending)
                return Result.Failure("Can Not Update Project Because It's Under Moderation", StatusCodes.Status400BadRequest);

            if (project.IsInvested)
                return Result.Failure("Can Not Update Project Because It's Invested", StatusCodes.Status400BadRequest);

            await _unitOfWork.ProjectUpdatesHistories.DeleteAsync(p => p.ProjectId == command.projectId);


            var projectUpdateHistory = new ProjectUpdateHistory()
            {
                Id = Guid.NewGuid(),
                ProjectId = command.projectId,
                Title = command.request.Title,
                ShortDescription = command.request.ShortDescription,
                Problem = command.request.Problem,
                SolutionSummary = command.request.SolutionSummary,
                Market = command.request.Market,
                NeededInvestment = command.request.NeededInvestment,
                EquityPercentage = command.request.EquityPercentage,
                ExecutionPlan = command.request.ExecutionPlan,
                FinancialDetails = command.request.FinancialDetails,
                BusinessModel = command.request.BusinessModel,
                MarketingStrategy = command.request.MarketingStrategy,
                Notes = command.request.Notes,
                Status = Status.Pending,
            };

            await _unitOfWork.ProjectUpdatesHistories.AddAsync(projectUpdateHistory);


            project.Status = Status.Pending;
            project.IsActive = false;
            project.UpdatedAt = DateTime.UtcNow;

            if (command.request.Images != null)
            {
                var projectImages = new List<ProjectUpdateImageHistory>();
                string imageURL = string.Empty;
                foreach (var image in command.request.Images)
                {
                    imageURL = await _fileService.UploadAsync(image, "Projects/Images");
                    projectImages.Add(new ProjectUpdateImageHistory { ImageUrl = imageURL, ProjectUpdateHistoryId = projectUpdateHistory.Id });
                }
                await _unitOfWork.ProjectUpdatesImagesHistories.AddRangeAsync(projectImages);
            }

            await _unitOfWork.CompleteAsync();
            return Result.Success("Project Updated Successfully, Wait For Moderators To Take An Action");
        }
    }
}
