using ProSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Domain.Entities
{
    public class FinancialVerificationHistory
    {
        [Key]
        public int Id { get; set; }
        public string InvestorEmail { get; set; }
        public string DocumentType { get; set; }
        public string DocumentURL { get; set; }
    }
}
