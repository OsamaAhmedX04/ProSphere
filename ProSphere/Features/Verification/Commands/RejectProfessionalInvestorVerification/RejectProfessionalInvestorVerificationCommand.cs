using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Verification.Commands.RejectProfessionalInvestorVerification
{
    public record RejectProfessionalInvestorVerificationCommand(
        string moderatorId, Guid professionalDocumentId, RejectProfessionalInvestorVerificationRequest request) : IRequest<Result>
    {
    }
}
