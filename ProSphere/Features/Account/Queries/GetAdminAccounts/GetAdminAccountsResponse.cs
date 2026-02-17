namespace ProSphere.Features.Account.Queries.GetAdminAccounts
{
    public class GetAdminAccountsResponse
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public bool IsSuperAdmin { get; set; }
    }
}
