namespace ProSphere.Features.Account.Commands.UpdateInvestorAccount
{
    public class UpdateInvestorAccountRequest
    {
        public IFormFile? ImageProfile { get; set; }
        public string? HeadLine { get; set; }
        public string? BIO { get; set; }
    }
}
