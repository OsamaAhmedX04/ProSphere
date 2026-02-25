namespace ProSphere.Features.Reports.Queries.GetReportReasons
{
    public class GetReportReasonsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public int NumberOfValues { get; set; }
    }
}
