using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.Extensions;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectModerating.Commands.RejectProjectCreation
{
    public class RejectProjectCreationCommandHandler : IRequestHandler<RejectProjectCreationCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<RejectProjectCreationRequest> _validator;

        public RejectProjectCreationCommandHandler(IUnitOfWork unitOfWork, IValidator<RejectProjectCreationRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result> Handle(RejectProjectCreationCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command.request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }

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

                updatedProject.Status = Status.Rejected;
                updatedProject.RejectionReason = command.request.Reason;
                project.IsActive = true;
                project.Status = Status.Approved;

            }

            // New Project
            else
            {
                project.Status = Status.Rejected;
                project.IsActive = false;
            }

            var moderationResult = new ProjectModeration
            {
                ModeratorId = command.moderatorId,
                ProjectId = command.projectId,
                Status = Status.Rejected,
                IsUpdate = project.UpdatedAt.HasValue,
                Reason = command.request.Reason
            };
            await _unitOfWork.ProjectsModerations.AddAsync(moderationResult);


            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Result.Failure("Error: Another Moderator Took An Action", StatusCodes.Status409Conflict);
            }

            return Result.Success("Project Is Rejected Successfully");
        }
    }
}
