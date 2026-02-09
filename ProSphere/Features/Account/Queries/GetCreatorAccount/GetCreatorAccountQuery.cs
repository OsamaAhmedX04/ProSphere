using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Account.Queries.GetCreatorAccount
{
    public record GetCreatorAccountQuery(string userId) : IRequest<Result<GetCreatorAccountResponse>>
    {
    }
}
