using MediatR;
using Microsoft.EntityFrameworkCore;
using ProSphere.Domain.Enums;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Reports.Commands.AcceptReportOnProject
{
    public class AcceptReportOnProjectCommandHandler : IRequestHandler<AcceptReportOnProjectCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AcceptReportOnProjectCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AcceptReportOnProjectCommand command, CancellationToken cancellationToken)
        {
            var report = await _unitOfWork.ReportedProjects.FirstOrDefaultAsync(r => r.Id == command.reportId);
            if (report is null)
                return Result.Failure("Report Not Found", StatusCodes.Status404NotFound);

            var project = await _unitOfWork.Projects.FirstOrDefaultAsync(p => p.Id == report.ProjectId);
            if (project is null)
                return Result.Failure("Project Not Found", StatusCodes.Status404NotFound);

            report.Status = Status.Approved;
            report.ReviewedAt = DateTime.UtcNow;
            report.ReviewedBy = command.moderatorId;

            project.IsBlocked = true;
            project.IsActive = false;

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Result.Failure("Error: Another Moderator Took An Action", StatusCodes.Status409Conflict);
            }

            await _unitOfWork.ReportedProjects.BulkDeleteAsync(r => r.ProjectId == report.ProjectId && r.Id != command.reportId);


            return Result.Success("Project Blocked Successfully");
        }
    }
}
