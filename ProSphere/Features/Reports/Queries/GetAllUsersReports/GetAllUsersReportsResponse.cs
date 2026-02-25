namespace ProSphere.Features.Reports.Queries.GetAllUsersReports
{
    public class GetAllUsersReportsResponse
    {
        public Guid Id { get; set; }
        public string TargetUserId { get; set; }
        public string TargetUserName { get; set; }
        public string TargetUserFullName { get; set; }
        public string TargetUserEmail { get; set; }
        public string Reason { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
