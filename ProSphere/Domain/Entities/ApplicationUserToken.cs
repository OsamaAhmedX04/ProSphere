using Microsoft.AspNetCore.Identity;

namespace ProSphere.Domain.Entities
{
    public class ApplicationUserToken : IdentityUserToken<string>
    {
        public DateTime ExpireDate { get; set; }
    }
}
