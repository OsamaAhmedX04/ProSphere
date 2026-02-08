using MediatR;
using ProSphere.Features.Verification.Commands.RejectIdentityVerification;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Verification.Commands.RejectFinancialInvestorVerification
{
    public record RejectFinancialInvestorVerificationCommand(
        string moderatorId, Guid financialDocumentId, RejectFinancialInvestorVerificationRequest request) : IRequest<Result>
    {
    }
}
