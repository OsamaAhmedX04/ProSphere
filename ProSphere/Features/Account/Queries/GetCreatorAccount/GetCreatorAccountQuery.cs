using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Account.Queries.GetCreatorAccount
{
    public record GetCreatorAccountQuery(string userName) : IRequest<Result<GetCreatorAccountResponse>>
    {
    }
}
