namespace ProSphere.Features.Verification.Commands.VerifyFinancialInvestor
{
    public class VerifyFinancialInvestorRequest
    {
        public int DocumentTypeId { get; set; }
        public IFormFile DocumentImage { get; set; }
        public string? Notes { get; set; }
    }
}
