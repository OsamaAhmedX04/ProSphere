
namespace ProSphere.Features.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string AssignedToModeratorId { get; set; }
    }
}
