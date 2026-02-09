using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Account.Queries.GetAdminAccount
{
    public record GetAdminAccountQuery(string userId) : IRequest<Result<GetAdminAccountResponse>>
    {
    }
}
