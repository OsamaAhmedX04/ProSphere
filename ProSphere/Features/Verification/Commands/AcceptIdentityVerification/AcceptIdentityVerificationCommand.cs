using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Verification.Commands.AcceptIdentityVerification
{
    public record AcceptIdentityVerificationCommand(string moderatorId, Guid identityDocumentId) : IRequest<Result>
    {
    }
}
