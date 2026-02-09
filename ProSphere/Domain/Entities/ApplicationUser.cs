using Microsoft.AspNetCore.Identity;
using ProSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProSphere.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(40)]
        public string FirstName { get; set; }

        [Required, MaxLength(40)]
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public bool IsVerified { get; set; }
        public List<Creator> Creators { get; set; } = new List<Creator>();
        public List<Investor> Investors { get; set; } = new List<Investor>();
        public List<Moderator> Moderators { get; set; } = new List<Moderator>();
        public List<Admin> Admins { get; set; } = new List<Admin>();
        public RefreshTokenAuth RefreshTokenAuth { get; set; }
        public ICollection<IdentityVerification> IdentityVerifications { get; set; } = new List<IdentityVerification>();
        

    }
}
