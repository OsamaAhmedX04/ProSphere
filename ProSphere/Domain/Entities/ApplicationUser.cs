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

        public bool IsActive { get; set; }

        public RefreshTokenAuth RefreshTokenAuth { get; set; }

    }
}
