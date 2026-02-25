using ProSphere.Domain.Enums;

namespace ProSphere.Features.ProjectManagement.Queries.GetCreatorProjects
{
    public class GetCreatorProjectsResponse
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public List<string> ImagesURL { get; set; } = new List<string>();
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public bool IsInvested { get; set; }
        public bool IsBlocked { get; set; }

    }
}
