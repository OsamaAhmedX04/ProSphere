using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Moderator.Queries.GetModeratorsEmail
{
    public record GetModeratorsEmailQuery(int pageNumber, bool? isUsed = null) : IRequest<Result<PageSourcePagination<GetModeratorsEmailResponse>>>
    {
    }
}
