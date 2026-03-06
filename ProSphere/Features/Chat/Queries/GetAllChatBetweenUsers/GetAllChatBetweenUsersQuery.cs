using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Chat.Queries.GetAllChatBetweenUsers
{
    public record GetAllChatBetweenUsersQuery(string firstUserId, string secondUserId, int pageNumber)
        : IRequest<Result<PageSourcePagination<GetAllChatBetweenUsersResponse>>>
    {
    }
}
