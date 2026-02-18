using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.CV.Queries.GetCVByUserId
{
    public record GetCVByUserIdQuery(string UserId) : IRequest<Result<GetCVByUserIdResponse>>
    {
    }
}
