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
        public InvestorLevel InvestorLevel { get; set; }
        public string? ImageProfileURL { get; set; }
        public bool IsVerified { get; set; }

        public FinancialVerification? FinancialVerification { get; set; }
        public ProfessionalVerification? ProfessionalVerification { get; set; }

    }
}
