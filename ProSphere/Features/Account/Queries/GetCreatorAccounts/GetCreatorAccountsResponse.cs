namespace ProSphere.Features.Account.Queries.GetCreatorAccounts
{
    public class GetCreatorAccountsResponse
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string? ImageProfileURL { get; set; }
        public string? HeadLine { get; set; }
        public bool IsVerified { get; set; }
    }
}
