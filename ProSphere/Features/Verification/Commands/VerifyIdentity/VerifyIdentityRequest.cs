namespace ProSphere.Features.Verification.Commands.VerifyIdentity
{
    public class VerifyIdentityRequest
    {
        public IFormFile IdFrontImage { get; set; }
        public IFormFile IdBackImage { get; set; }
        public IFormFile SelfieWithId { get; set; }
    }
}
