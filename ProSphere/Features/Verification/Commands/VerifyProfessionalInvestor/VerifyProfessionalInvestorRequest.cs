namespace ProSphere.Features.Verification.Commands.VerifyProfessionalInvestor
{
    public class VerifyProfessionalInvestorRequest
    {
        public string DocumentType { get; set; }
        public IFormFile DocumentImage { get; set; }

        public string? Notes { get; set; }
    }
}
