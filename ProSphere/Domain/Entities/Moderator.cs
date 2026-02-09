using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Domain.Entities
{
    public class Moderator
    {
        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<IdentityVerification> ReviewedIdentityVerifications { get; set; } = new List<IdentityVerification>();
        public ICollection<FinancialVerification> ReviewedFinancialVerifications { get; set; } = new List<FinancialVerification>();
        public ICollection<ProfessionalVerification> ReviewedProfessionalVerifications { get; set; } = new List<ProfessionalVerification>();
    }
}
