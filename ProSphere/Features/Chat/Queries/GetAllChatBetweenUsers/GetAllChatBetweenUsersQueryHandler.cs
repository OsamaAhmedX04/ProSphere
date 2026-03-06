using MediatR;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Chat.Queries.GetAllChatBetweenUsers
{
    public class GetAllChatBetweenUsersQueryHandler
        : IRequestHandler<GetAllChatBetweenUsersQuery, Result<PageSourcePagination<GetAllChatBetweenUsersResponse>>>
    {
        private readonly IChatRepository _chatRepository;

        public GetAllChatBetweenUsersQueryHandler(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<Result<PageSourcePagination<GetAllChatBetweenUsersResponse>>> 
            Handle(GetAllChatBetweenUsersQuery query, CancellationToken cancellationToken)
        {
            var result = await _chatRepository.GetMessagesBetweenUsers(query.firstUserId, query.secondUserId, query.pageNumber, 25);

            return Result<PageSourcePagination<GetAllChatBetweenUsersResponse>>.Success(result, "Paginated Chat Content Retrieved Successfully");
        }
    }
}
