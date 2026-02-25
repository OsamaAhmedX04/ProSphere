using MediatR;
using Microsoft.EntityFrameworkCore;
using ProSphere.Domain.Enums;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Reports.Commands.RejectReportOnProject
{
    public class RejectReportOnProjectCommandHandler : IRequestHandler<RejectReportOnProjectCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RejectReportOnProjectCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(RejectReportOnProjectCommand command, CancellationToken cancellationToken)
        {
            var report = await _unitOfWork.ReportedProjects.FirstOrDefaultAsync(r => r.Id == command.reportId);
            if (report is null)
                return Result.Failure("Report Not Found", StatusCodes.Status404NotFound);

            report.Status = Status.Rejected;
            report.ReviewedAt = DateTime.UtcNow;
            report.ReviewedBy = command.moderatorId;

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Result.Failure("Error: Another Moderator Took An Action", StatusCodes.Status409Conflict);
            }

            return Result.Success("Project Report Rejected Successfully");
        }
    }
}
