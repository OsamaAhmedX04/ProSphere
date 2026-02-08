using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Verification.Commands.RejectIdentityVerification
{
    public record RejectIdentityVerificationCommand
        (string moderatorId, Guid identityDocumentId, RejectIdentityVerificationRequest request) : IRequest<Result>
    {
    }
}
