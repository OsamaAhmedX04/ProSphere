using System.ComponentModel.DataAnnotations;

namespace ProSphere.Domain.Entities
{
    public class ProfessionalVerificationHistory
    {
        [Key]
        public int Id { get; set; }
        public string InvestorEmail { get; set; }
        public string DocumentType { get; set; }
        public string DocumentURL { get; set; }
    }
}
