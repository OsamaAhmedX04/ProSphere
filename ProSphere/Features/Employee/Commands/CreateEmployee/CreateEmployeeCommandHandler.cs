using FluentValidation;
using MediatR;
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

        public CreateEmployeeCommandHandler(IUnitOfWork unitOfWork, IValidator<CreateEmployeeRequest> validator,
            IEmailSenderService emailSenderService)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _emailSenderService = emailSenderService;
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
                selector: m => new { m.Id });
            if (moderator is null)
                return Result.Failure("Moderator Account Is Not Available", StatusCodes.Status404NotFound);


            var isDuplicatedEmail = await _unitOfWork.Employees.AnyAsync(e => e.Email == command.request.Email);
            if (isDuplicatedEmail)
                return Result.Failure("Employee Is Email Already Exist", StatusCodes.Status400BadRequest);


            var newEmployee = new Domain.Entities.Employee
            {
                Name = command.request.Name,
                Email = command.request.Email,
                Phone = command.request.Phone,
                Country = command.request.Country,
                AssignedTo = command.request.AssignedToModeratorId,
            };

            await _unitOfWork.Employees.AddAsync(newEmployee);
            await _unitOfWork.CompleteAsync();

            _emailSenderService.SendWelcomeEmployeeMail(command.request.Email, command.request.Name);

            return Result.Success("New Employee Has Been Created Successfully");
        }
    }
}
