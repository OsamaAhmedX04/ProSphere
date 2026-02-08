using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Verification.Commands.AcceptProfessionalInvestorVerification
{
    public record AcceptProfessionalInvestorVerificationCommand(string moderatorId, Guid professionalDocumentId) : IRequest<Result>
    {
    }
}
