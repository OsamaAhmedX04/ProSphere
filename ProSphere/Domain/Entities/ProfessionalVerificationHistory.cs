using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Domain.Entities
{
    public class ProfessionalVerificationHistory
    {
        [Key]
        public int Id { get; set; }
        public string InvestorEmail { get; set; }

        [ForeignKey("DocumentType")]
        public int DocumentTypeId { get; set; }
        public ProfessionalDocumentType DocumentType { get; set; }
        public string DocumentURL { get; set; }
    }
}
