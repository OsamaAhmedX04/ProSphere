using System.ComponentModel.DataAnnotations;

namespace ProSphere.Domain.Entities
{
    public class ProfessionalDocumentType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<ProfessionalVerification> ProfessionalVerifications { get; set; } = new List<ProfessionalVerification>();
        public ICollection<ProfessionalVerificationHistory> ProfessionalVerificationHistories { get; set; } = new List<ProfessionalVerificationHistory>();

    }
}
