using FluentValidation;
using MediatR;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.Extensions;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Reports.Commands.SendReportOnProject
{
    public class SendReportOnProjectCommandHandler : IRequestHandler<SendReportOnProjectCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<SendReportOnProjectRequest> _validator;

        public SendReportOnProjectCommandHandler(IUnitOfWork unitOfWork, IValidator<SendReportOnProjectRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result> Handle(SendReportOnProjectCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command.request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }

            var project = await _unitOfWork.Projects.FirstOrDefaultAsync(p => p.Id == command.projectId);
            if (project is null)
                return Result.Failure("Project Not Found", StatusCodes.Status404NotFound);
            if (project.IsBlocked)
                return Result.Failure("Project Is Already Blocked", StatusCodes.Status400BadRequest);

            if (!Enum.TryParse<ReportReason>(command.request.Reason, true, out var parsedReason))
                return Result.Failure("Invalid Report Reason", StatusCodes.Status400BadRequest);

            var report = await _unitOfWork.ReportedProjects
                .FirstOrDefaultAsync(x => x.ProjectId == command.projectId && x.ReporterId == command.reporterId);

            if (report != null)
                _unitOfWork.ReportedProjects.Delete(report.Id);
            
            var newReport = new ReportedProject
            {
                Description = command.request.Description,
                Reason = parsedReason,
                ProjectId = command.projectId,
                ReporterId = command.reporterId,
                Status = Status.Pending,
            };

            await _unitOfWork.ReportedProjects.AddAsync(newReport);

            await _unitOfWork.CompleteAsync();

            return Result.Success("Project Reported Successfully");
        }
    }
}
