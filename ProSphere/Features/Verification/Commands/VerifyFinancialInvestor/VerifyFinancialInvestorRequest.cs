using ProSphere.Domain.Enums;

namespace ProSphere.Features.Verification.Commands.VerifyFinancialInvestor
{
    public class VerifyFinancialInvestorRequest
    {
        public string DocumentType { get; set; }
        public IFormFile DocumentImage { get; set; }
        public string? Notes { get; set; }
    }
}
