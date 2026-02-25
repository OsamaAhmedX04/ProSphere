namespace ProSphere.Features.AccessProjectRequest.Queries.GetAccessRequestsOnProject
{
    public class GetAccessRequestsOnProjectResponse
    {
        public Guid RequestId { get; set; }
        public Guid ProjectId { get; set; }
        public string InvestorId { get; set; }
        public string InvestorFullName { get; set; }
        public string? InvestorImageProfileURL { get; set; }
        public string ProjectTitle { get; set; }
        public string Status { get; set; }
    }
}
