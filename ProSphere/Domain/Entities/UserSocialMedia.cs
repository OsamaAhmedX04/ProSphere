using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Domain.Entities
{
    public class UserSocialMedia
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string? LinkedInURL { get; set; }
        public string? FacebookInURL { get; set; }
        public string? GitHubURL { get; set; }
        public string? FirstOtherName { get; set; }
        public string? FirstOtherNameURL { get; set; }
        public string? SecondOtherName { get; set; }
        public string? SecondOtherNameURL { get; set; }
        public string? ThirdOtherName { get; set; }
        public string? ThirdOtherNameURL { get; set; }
    }
}
