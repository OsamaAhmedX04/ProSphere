namespace ProSphere.Features.Account.Commands.UpdateCreatorAccount
{
    public class UpdateCreatorAccountRequest
    {
        public IFormFile? ImageProfile { get; set; }
        public string? HeadLine { get; set; }
        public string? BIO { get; set; }
    }
}
