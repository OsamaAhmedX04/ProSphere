using FluentValidation;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using ProSphere.Domain.Constants.CacheConstants;
using ProSphere.Extensions;
using ProSphere.ExternalServices.Interfaces.Email;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateEmployeeRequest> _validator;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IMemoryCache _cache;
        private readonly ILogger<CreateEmployeeCommandHandler> _logger;

        public CreateEmployeeCommandHandler(IUnitOfWork unitOfWork, IValidator<CreateEmployeeRequest> validator,
            IEmailSenderService emailSenderService, IMemoryCache cache, ILogger<CreateEmployeeCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _emailSenderService = emailSenderService;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command.request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }

            var moderator = await _unitOfWork.Moderators.GetEnhancedAsync(
                filter: m => m.IsUsed == false && m.Id == command.request.AssignedToModeratorId,
                selector: m => m);
            if (moderator is null)
                return Result.Failure("Moderator Account Is Not Available", StatusCodes.Status404NotFound);


            var isDuplicatedEmail = await _unitOfWork.Employees.AnyAsync(e => e.Email == command.request.Email);
            if (isDuplicatedEmail)
                return Result.Failure("Employee Email Is Already Exist", StatusCodes.Status400BadRequest);


            var newEmployee = new Domain.Entities.Employee
            {
                Name = command.request.Name,
                Email = command.request.Email,
                Phone = command.request.Phone,
                Country = command.request.Country,
                AssignedTo = command.request.AssignedToModeratorId,
                IsActive = true
            };

            moderator.IsUsed = true;

            await _unitOfWork.Employees.AddAsync(newEmployee);
            await _unitOfWork.CompleteAsync();

            _cache.Remove(CacheKey.GetModeratorAccountKey(moderator.Id));
            _cache.Remove(CacheKey.GetModeratorAvailableEmailsKey);

            _logger.LogInformation("New Employee Created With Id: {EmployeeId} And Assigned To Moderator Id: {ModeratorId}",
                newEmployee.Id, moderator.Id);

            _emailSenderService.SendWelcomeEmployeeMail(command.request.Email, command.request.Name);

            return Result.Success("New Employee Has Been Created Successfully");
        }
    }
}
