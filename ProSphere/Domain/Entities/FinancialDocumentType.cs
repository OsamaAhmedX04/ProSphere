using System.ComponentModel.DataAnnotations;

namespace ProSphere.Domain.Entities
{
    public class FinancialDocumentType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<FinancialVerification> FinancialVerifications { get; set; } = new List<FinancialVerification>();
        public ICollection<FinancialVerificationHistory> FinancialVerificationHistories { get; set; } = new List<FinancialVerificationHistory>();
    }
}
