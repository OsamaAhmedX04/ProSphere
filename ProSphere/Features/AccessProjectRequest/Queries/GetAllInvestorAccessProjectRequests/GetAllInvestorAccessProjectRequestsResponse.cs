namespace ProSphere.Features.AccessProjectRequest.Queries.GetAllInvestorAccessProjectRequests
{
    public class GetAllInvestorAccessProjectRequestsResponse
    {
        public Guid RequestId { get; set; }
        public Guid ProjectId { get; set; }
        public string CreatorId { get; set; }
        public string CreatorFullName { get; set; }
        public string? CreatorImageProfile { get; set; }
        public string ProjectTitle { get; set; }
        public string Status { get; set; }
    }
}
