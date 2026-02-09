using MediatR;
using ProSphere.Features.Account.Queries.GetAdminAccount;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Account.Queries.GetInvestorAccount
{
    public record GetInvestorAccountQuery(string userId) : IRequest<Result<GetInvestorAccountResponse>>
    {
    }
}
