using LinqKit;
using MediatR;
using ProSphere.Domain.Entities;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;
using System.Linq.Expressions;

namespace ProSphere.Features.Chat.Queries.GetAllContactsForUser
{
    public class GetAllContactsForUserQueryHandler
        : IRequestHandler<GetAllContactsForUserQuery, Result<PageSourcePagination<GetAllContactsForUserResponse>>>
    {
        private readonly IChatRepository _chatRepository;

        public GetAllContactsForUserQueryHandler(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }


        public async Task<Result<PageSourcePagination<GetAllContactsForUserResponse>>>
            Handle(GetAllContactsForUserQuery query, CancellationToken cancellationToken)
        {
            Expression<Func<Conversation, bool>> filter = c => true;
            if (!string.IsNullOrEmpty(query.contactName))
                filter = filter.And(c => c.Creator.FullName.Contains(query.contactName) || c.Investor.FullName.Contains(query.contactName));

            var result = await _chatRepository.GetUserContacts(query.userId, filter, query.pageNumber, pageSize: 20);

            return Result<PageSourcePagination<GetAllContactsForUserResponse>>
                .Success(result, "Paginated Contacts Retrieved Successfully");
        }
    }
}
