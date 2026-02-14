namespace ProSphere.Features.Employee.Queries.GetAllEmployees
{
    public class GetAllEmployeesResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string? ModeratorAccountId { get; set; }
        public string? ModeratorAccountCode { get; set; }
        public DateTime StartWorkAt { get; set; }
        public DateTime? EndWorkAt { get; set; }
    }
}
