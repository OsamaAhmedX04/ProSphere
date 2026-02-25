using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Features.Reports.Queries.GetAllProjectsReports
{
    public class GetAllProjectsReportsResponse
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public string Reason { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
