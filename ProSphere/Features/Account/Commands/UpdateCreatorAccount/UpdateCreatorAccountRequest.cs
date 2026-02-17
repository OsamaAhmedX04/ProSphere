namespace ProSphere.Features.Account.Commands.UpdateCreatorAccount
{
    public class UpdateCreatorAccountRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public IFormFile? ImageProfile { get; set; }
        public string? HeadLine { get; set; }
        public string? BIO { get; set; }
    }
}
