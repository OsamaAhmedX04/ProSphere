namespace ProSphere.Features.Employee.Commands.AssignToModerator
{
    public class AssignToModeratorRequest
    {
        public Guid EmployeeId { get; set; }
        public string ModeratorId { get; set; }
    }
}
