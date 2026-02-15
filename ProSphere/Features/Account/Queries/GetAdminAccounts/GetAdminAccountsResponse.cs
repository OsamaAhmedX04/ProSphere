namespace ProSphere.Features.Account.Queries.GetAdminAccounts
{
    public class GetAdminAccountsResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public bool IsSuperAdmin { get; set; }
    }
}
