namespace ProSphere.Features.Account.Commands.UpdateInvestorAccount
{
    public class UpdateInvestorAccountRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public IFormFile? ImageProfile { get; set; }
        public string? HeadLine { get; set; }
        public string? BIO { get; set; }
    }
}
