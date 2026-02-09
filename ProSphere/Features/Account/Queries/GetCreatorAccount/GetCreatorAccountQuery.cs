using MediatR;
using ProSphere.Features.Account.Queries.GetAdminAccount;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Account.Queries.GetCreatorAccount
{
    public record GetCreatorAccountQuery(string userId) : IRequest<Result<GetCreatorAccountResponse>>
    {
    }
}
