namespace ProSphere.Features.Verification.Commands.VerifyProfessionalInvestor
{
    public class VerifyProfessionalInvestorRequest
    {
        public int DocumentTypeId { get; set; }
        public IFormFile DocumentImage { get; set; }
        public string? Notes { get; set; }
    }
}
