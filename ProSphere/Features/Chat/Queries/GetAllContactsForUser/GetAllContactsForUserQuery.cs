using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Chat.Queries.GetAllContactsForUser
{
    public record GetAllContactsForUserQuery(int pageNumber, string userId, string? contactName = null)
        : IRequest<Result<PageSourcePagination<GetAllContactsForUserResponse>>>
    {
    }
}
