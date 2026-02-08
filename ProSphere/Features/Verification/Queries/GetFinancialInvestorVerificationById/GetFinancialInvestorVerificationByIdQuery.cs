using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Verification.Queries.GetFinancialInvestorVerificationById
{
    public record GetFinancialInvestorVerificationByIdQuery(Guid financialDocumentId)
        : IRequest<Result<GetFinancialInvestorVerificationByIdResponse>>
    {
    }
}
