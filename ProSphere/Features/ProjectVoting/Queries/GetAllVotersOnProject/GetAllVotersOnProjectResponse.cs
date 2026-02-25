namespace ProSphere.Features.ProjectVoting.Queries.GetAllVotersOnProject
{
    public class GetAllVotersOnProjectResponse
    {
        public string CreatorId { get; set; }
        public string CreatorName { get; set; }
        public string UserName { get; set; }
        public string? HeadLine { get; set; }
        public string? UserProfileImageURL { get; set; }
    }
}
