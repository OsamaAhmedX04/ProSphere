using MediatR;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Employee.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEmployeeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.Employees.FirstOrDefaultAsync(e => e.Id == command.employeeId);
            if (employee is null)
                return Result.Failure("User Not Found", StatusCodes.Status404NotFound);

            var moderator = await _unitOfWork.Moderators.FirstOrDefaultAsync(m => m.Id == employee.AssignedTo);

            if(moderator is null)
                return Result.Failure("User Not Found", StatusCodes.Status404NotFound);

            moderator.IsUsed = false;
            employee.IsActive = false;
            employee.IsDeleted = true;
            employee.AssignedTo = null;
            employee.EndWorkAt = DateTime.UtcNow;


            await _unitOfWork.CompleteAsync();

            return Result.Success("Employee Is Deleted Successfully");
        }
    }
}
