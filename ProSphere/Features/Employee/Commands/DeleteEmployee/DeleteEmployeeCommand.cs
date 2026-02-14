using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Employee.Commands.DeleteEmployee
{
    public record DeleteEmployeeCommand(Guid employeeId) : IRequest<Result>
    {
    }
}
