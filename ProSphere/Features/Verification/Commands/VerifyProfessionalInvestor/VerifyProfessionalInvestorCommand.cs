using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Verification.Commands.VerifyProfessionalInvestor
{
    public record VerifyProfessionalInvestorCommand(string investorId, VerifyProfessionalInvestorRequest request) : IRequest<Result>
    {
    }
}
