using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Verification.Commands.AcceptFinancialInvestorVerification
{
    public record AcceptFinancialInvestorVerificationCommand(string moderatorId, Guid financialDocumentId) : IRequest<Result>
    {
    }
}
