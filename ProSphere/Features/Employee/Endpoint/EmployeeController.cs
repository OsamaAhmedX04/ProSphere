using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProSphere.Features.Employee.Commands.CreateEmployee;
using ProSphere.Features.Employee.Commands.DeleteEmployee;
using ProSphere.Features.Employee.Queries.GetAllEmployees;

namespace ProSphere.Features.Employee.Endpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ISender _sender;

        public EmployeeController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees(int pageNumber, string? name = null, string? country = null)
        {
            var query = new GetAllEmployeesQuery(pageNumber, name, country);
            var result = await _sender.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewEmployee(CreateEmployeeRequest request)
        {
            var command = new CreateEmployeeCommand(request);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var command = new DeleteEmployeeCommand(id);
            var result = await _sender.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
}
