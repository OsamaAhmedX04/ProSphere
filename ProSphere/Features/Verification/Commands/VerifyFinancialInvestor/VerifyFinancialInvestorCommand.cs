using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Verification.Commands.VerifyFinancialInvestor
{
    public record VerifyFinancialInvestorCommand(string investorId, VerifyFinancialInvestorRequest request) : IRequest<Result>
    {
    }
}
