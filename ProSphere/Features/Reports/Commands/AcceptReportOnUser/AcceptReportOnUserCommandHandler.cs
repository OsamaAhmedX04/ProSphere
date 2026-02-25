using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.Jobs.Ban.RemoveBan;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Reports.Commands.AcceptReportOnUser
{
    public class AcceptReportOnUserCommandHandler : IRequestHandler<AcceptReportOnUserCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public AcceptReportOnUserCommandHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<Result> Handle(AcceptReportOnUserCommand command, CancellationToken cancellationToken)
        {
            var report = await _unitOfWork.ReportedUsers.FirstOrDefaultAsync(r => r.Id == command.reportId);
            if (report is null)
                return Result.Failure("Report Not Found", StatusCodes.Status404NotFound);

            var user = await _userManager.FindByIdAsync(report.UserId);
            if (user is null)
                return Result.Failure("Target User Not Found", StatusCodes.Status404NotFound);

            report.Status = Status.Approved;
            user.IsBanned = true;

            var banData = new BannedUser
            {
                NumberOfBannedDays = command.numberOfBanDays,
                Reason = report.Reason,
                UserId = user.Id,
            };

            await _unitOfWork.BannedUsers.AddAsync(banData);

            try
            {
                await _unitOfWork.CompleteAsync();
                await _userManager.UpdateAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Result.Failure("Error: Another Moderator Took An Action", StatusCodes.Status409Conflict);
            }

            await _unitOfWork.ReportedUsers.BulkDeleteAsync(r => r.UserId == report.UserId && r.Id != command.reportId);

            BackgroundJob.Schedule<IRemoveBanJob>(
                serivce => serivce.RemoveBan(user.Id),
                TimeSpan.FromDays(command.numberOfBanDays)
                );

            return Result.Success("User Banned Successfully");
        }
    }
}
