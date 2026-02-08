using ProSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Domain.Entities
{
    public class IdentityVerificationHistory
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserEmail { get; set; }
        public string IdFrontImageURL { get; set; }
        public string IdBackImageURL { get; set; }
        public string SelfieWithIdURL { get; set; }
    }
}
