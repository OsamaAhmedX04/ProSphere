namespace ProSphere.Features.Account.Queries.GetInvestorAccounts
{
    public class GetInvestorAccountsResponse
    {
        public string UserName { get; set; }
        public string? ImageProfileURL { get; set; }
        public string? HeadLine { get; set; }
        public bool IsVerified { get; set; }
        public bool IsFinancail { get; set; }
        public bool IsProfessional { get; set; }
    }
}
