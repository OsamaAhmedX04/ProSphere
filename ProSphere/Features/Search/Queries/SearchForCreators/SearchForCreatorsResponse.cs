namespace ProSphere.Features.Search.Queries.SearchForCreators
{
    public class SearchForCreatorsResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string? ImageProfileURL { get; set; }
        public string? HeadLine { get; set; }
        public bool IsVerified { get; set; }
    }
}
