using FluentValidation;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using ProSphere.Domain.Constants.CacheConstants;
using ProSphere.Extensions;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Employee.Commands.AssignToModerator
{
    public class AssignToModeratorCommandHandler : IRequestHandler<AssignToModeratorCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<AssignToModeratorRequest> _validator;
        private readonly IMemoryCache _cache;
        private readonly ILogger<AssignToModeratorCommandHandler> _logger;

        public AssignToModeratorCommandHandler(IUnitOfWork unitOfWork, IValidator<AssignToModeratorRequest> validator, IMemoryCache cache, ILogger<AssignToModeratorCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result> Handle(AssignToModeratorCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command.request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }
            var employee = await _unitOfWork.Employees.FirstOrDefaultAsync(e => e.Id == command.request.EmployeeId);
            var moderator = await _unitOfWork.Moderators.FirstOrDefaultAsync(e => e.Id == command.request.ModeratorId);

            if (employee is null || employee.IsDeleted)
                return Result.Failure("Employee Not Found", StatusCodes.Status404NotFound);

            if (moderator is null)
                return Result.Failure("Moderator Not Found", StatusCodes.Status404NotFound);

            if (moderator.IsUsed)
                return Result.Failure("Moderator is already assigned to another Employee", StatusCodes.Status400BadRequest);

            if (employee.IsActive)
                return Result.Failure("Employee is already assigned to another Moderator", StatusCodes.Status400BadRequest);

            employee.IsActive = true;
            moderator.IsUsed = true;

            employee.AssignedTo = moderator.Id;
            employee.EndWorkAt = null;

            await _unitOfWork.CompleteAsync();

            _cache.Remove(CacheKey.GetModeratorAccountKey(moderator.Id));
            _cache.Remove(CacheKey.GetModeratorAvailableEmailsKey);

            _logger.LogInformation("Employee with ID {EmployeeId} assigned to Moderator with ID {ModeratorId}", employee.Id, moderator.Id);

            return Result.Success("Employee assigned to Moderator successfully");
        }
    }
}
