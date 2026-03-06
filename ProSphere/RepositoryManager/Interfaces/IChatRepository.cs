using ProSphere.Domain.Entities;
using ProSphere.Features.Chat.Queries.GetAllChatBetweenUsers;
using ProSphere.Features.Chat.Queries.GetAllContactsForUser;
using ProSphere.RepositoryManager.Pagination;
using System.Linq.Expressions;

namespace ProSphere.RepositoryManager.Interfaces
{
    public interface IChatRepository
    {
        Task<PageSourcePagination<GetAllContactsForUserResponse>> GetUserContacts(
            string userId, Expression<Func<Conversation, bool>> filter,
            int pageNumber, int pageSize);
        Task<PageSourcePagination<GetAllChatBetweenUsersResponse>> GetMessagesBetweenUsers(string firstUserId, string secondUserId, int pageNumber, int pageSize);
    }
}
