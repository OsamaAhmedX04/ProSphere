namespace ProSphere.Features.Account.Queries.GetModeratorAccounts
{
    public class GetModeratorAccountsResponse
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public bool IsUsed { get; set; }
        public string Code { get; set; }
    }
}
