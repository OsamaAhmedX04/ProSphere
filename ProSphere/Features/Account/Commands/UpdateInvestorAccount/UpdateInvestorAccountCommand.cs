using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Account.Commands.UpdateInvestorAccount
{
    public record UpdateInvestorAccountCommand(string investorId, UpdateInvestorAccountRequest request) : IRequest<Result>
    {
    }
}
