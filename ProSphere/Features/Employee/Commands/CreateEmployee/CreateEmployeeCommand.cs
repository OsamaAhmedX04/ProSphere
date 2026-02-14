using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Employee.Commands.CreateEmployee
{
    public record CreateEmployeeCommand(CreateEmployeeRequest request) : IRequest<Result>
    {
    }
}
