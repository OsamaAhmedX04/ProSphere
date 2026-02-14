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
        public string? FacebookURL { get; set; }
        public string? GitHubURL { get; set; }
        public string? FirstPlatformName { get; set; }
        public string? FirstPlatformURL { get; set; }
        public string? SecondPlatformName { get; set; }
        public string? SecondPlatformURL { get; set; }
        public string? ThirdPlatformName { get; set; }
        public string? ThirdPlatformURL { get; set; }
    }
}
