namespace ProSphere.Features.Account.Queries.GetModeratorAccount
{
    public class GetModeratorAccountResponse
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public bool IsUsed { get; set; }
        public string Code { get; set; }
        public Guid? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmployeeEmail { get; set; }
        public string? EmployeePhone { get; set; }
        public string? EmployeeCountry { get; set; }

    }
}
