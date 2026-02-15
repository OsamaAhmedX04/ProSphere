using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Moderator.Queries.GetModeratorsAvailableEmail
{
    public record GetModeratorsAvailableEmailQuery() : IRequest<Result<List<GetModeratorsAvailableEmailResponse>>>
    {
    }
}
