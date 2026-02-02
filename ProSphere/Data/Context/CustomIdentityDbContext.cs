using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Context
{
    public abstract class CustomIdentityDbContext : IdentityDbContext
        <ApplicationUser,
        IdentityRole,
        string,
        IdentityUserClaim<string>,
        IdentityUserRole<string>,
        IdentityUserLogin<string>,
        IdentityRoleClaim<string>,
        IdentityUserToken>
    {
        protected CustomIdentityDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
