namespace ProSphere.Features.Account.Queries.GetCreatorAccount
{
    public class GetCreatorAccountResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ImageProfileURL { get; set; }
        public string? HeadLine { get; set; }
        public string? BIO { get; set; }
        public string Gender { get; set; }
        public bool IsVerified { get; set; }
    }
}
