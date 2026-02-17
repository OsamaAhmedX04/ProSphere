using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Account.Queries.GetModeratorAccount
{
    public record GetModeratorAccountQuery(string userName) : IRequest<Result<GetModeratorAccountResponse>>
    {
    }
}
