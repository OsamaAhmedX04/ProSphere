using FluentValidation;
using MediatR;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.Extensions;
using ProSphere.ExternalServices.Interfaces.FileStorage;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectManagement.Commands.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateProjectRequest> _validator;
        private readonly IFileService _fileService;

        public CreateProjectCommandHandler
            (IUnitOfWork unitOfWork, IValidator<CreateProjectRequest> validator, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _fileService = fileService;
        }

        public async Task<Result> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command.request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }

            var isCreatorExist = await _unitOfWork.Creators.IsExistAsync(command.creatorId);
            if (!isCreatorExist) return Result.Failure("Creator Not Found", StatusCodes.Status404NotFound);

            var newProject = new Project
            {
                Id = Guid.NewGuid(),
                CreatorId = command.creatorId,
                Title = command.request.Title,
                ShortDescription = command.request.ShortDescription,
                Problem = command.request.Problem,
                SolutionSummary = command.request.SolutionSummary,
                Market = command.request.Market,
                NeededInvestment = command.request.NeededInvestment,
                EquityPercentage = command.request.EquityPercentage,
                Status = Status.Pending,

                Details = new ProjectDetail
                {
                    Id = Guid.NewGuid(),
                    ExecutionPlan = command.request.ExecutionPlan,
                    FinancialDetails = command.request.FinancialDetails,
                    BusinessModel = command.request.BusinessModel,
                    MarketingStrategy = command.request.MarketingStrategy,
                    Notes = command.request.Notes
                }
            };

            var projectImages = new List<ProjectImage>();
            if (command.request.Images != null)
            {
                string imageURL = string.Empty;
                foreach (var image in command.request.Images)
                {
                    imageURL = await _fileService.UploadAsync(image, "Projects/Images");
                    projectImages.Add(new ProjectImage { ImageUrl = imageURL, ProjectId = newProject.Id });
                }
                newProject.Images = projectImages;
            }

            await _unitOfWork.Projects.AddAsync(newProject);
            await _unitOfWork.CompleteAsync();

            return Result.Success("Project Created Successfully, Wait For Moderators To Take An Action");
        }
    }
}
