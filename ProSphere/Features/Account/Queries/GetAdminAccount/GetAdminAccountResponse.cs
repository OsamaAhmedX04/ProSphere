using ProSphere.Domain.Enums;

namespace ProSphere.Features.Account.Queries.GetAdminAccount
{
    public class GetAdminAccountResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public bool IsSuperAdmin { get; set; }
    }
}
