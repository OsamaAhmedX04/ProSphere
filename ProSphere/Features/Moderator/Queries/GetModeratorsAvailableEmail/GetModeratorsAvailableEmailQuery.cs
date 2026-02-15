using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Moderator.Queries.GetModeratorsAvailableEmail
{
    public record GetModeratorsAvailableEmailQuery() : IRequest<Result<List<GetModeratorsAvailableEmailResponse>>>
    {
    }
}
