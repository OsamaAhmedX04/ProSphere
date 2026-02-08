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
        public Creator Creator { get; set; }
        public Investor Investor { get; set; }
        public RefreshTokenAuth RefreshTokenAuth { get; set; }
        public ICollection<IdentityVerification> IdentityVerifications { get; set; } = new List<IdentityVerification>();
        public ICollection<IdentityVerification> ReviewedIdentityVerifications { get; set; } = new List<IdentityVerification>();
        public ICollection<FinancialVerification> ReviewedFinancialVerifications { get; set; } = new List<FinancialVerification>();
        public ICollection<ProfessionalVerification> ReviewedProfessionalVerifications { get; set; } = new List<ProfessionalVerification>();

    }
}
