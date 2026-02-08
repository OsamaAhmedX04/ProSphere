using ProSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Domain.Entities
{
    public class Investor
    {
        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }
        public ApplicationUser User { get; set; }
        public string UserName { get; set; }
        public InvestorLevel InvestorLevel { get; set; }
        public string? ImageProfileURL { get; set; }

        public ICollection<FinancialVerification> FinancialVerifications { get; set; } = new List<FinancialVerification>();
        public ICollection<ProfessionalVerification> ProfessionalVerifications { get; set; } = new List<ProfessionalVerification>();

    }
}
