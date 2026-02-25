using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.Extensions;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Reports.Commands.SendReportOnUser
{
    public class SendReportOnUserCommandHandler : IRequestHandler<SendReportOnUserCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<SendReportOnUserRequest> _validator;
        private readonly UserManager<ApplicationUser> _userManager;

        public SendReportOnUserCommandHandler
            (IUnitOfWork unitOfWork, IValidator<SendReportOnUserRequest> validator, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _userManager = userManager;
        }

        public async Task<Result> Handle(SendReportOnUserCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command.request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }


            if (!Enum.TryParse<ReportReason>(command.request.Reason, true, out var parsedReason))
                return Result.Failure("Invalid Report Reason", StatusCodes.Status400BadRequest);

            var report = await _unitOfWork.ReportedUsers
                .FirstOrDefaultAsync(x => x.UserId == command.targetUserId && x.ReporterId == command.reporterId);

            if (report != null)
                _unitOfWork.ReportedUsers.Delete(report.Id);

            var user = await _userManager.FindByIdAsync(command.targetUserId);
            if (user is null) return Result.Failure("User Not Found", StatusCodes.Status404NotFound);
            if (user.IsBanned) return Result.Failure("User Already Banned", StatusCodes.Status400BadRequest);


            var newReport = new ReportedUser
            {
                Description = command.request.Description,
                Reason = parsedReason,
                UserId = command.targetUserId,
                ReporterId = command.reporterId,
                Status = Status.Pending,
            };

            await _unitOfWork.ReportedUsers.AddAsync(newReport);

            await _unitOfWork.CompleteAsync();

            return Result.Success("User Reported Successfully");
        }
    }
}
