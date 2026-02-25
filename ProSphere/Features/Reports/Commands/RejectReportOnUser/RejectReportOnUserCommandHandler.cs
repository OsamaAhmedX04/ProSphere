using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.Jobs.Ban.RemoveBan;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Reports.Commands.RejectReportOnUser
{
    public class RejectReportOnUserCommandHandler : IRequestHandler<RejectReportOnUserCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RejectReportOnUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(RejectReportOnUserCommand command, CancellationToken cancellationToken)
        {
            var report = await _unitOfWork.ReportedUsers.FirstOrDefaultAsync(r => r.Id == command.reportId);
            if (report is null)
                return Result.Failure("Report Not Found", StatusCodes.Status404NotFound);


            report.Status = Status.Rejected;

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Result.Failure("Error: Another Moderator Took An Action", StatusCodes.Status409Conflict);
            }

            return Result.Success("Report On User Rejected Successfully");
        }
    }
}
