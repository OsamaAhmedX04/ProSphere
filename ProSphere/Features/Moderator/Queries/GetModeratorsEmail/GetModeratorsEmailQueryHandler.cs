using MediatR;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;
using System.Linq.Expressions;

namespace ProSphere.Features.Moderator.Queries.GetModeratorsEmail
{
    public class GetModeratorsEmailQueryHandler
        : IRequestHandler<GetModeratorsEmailQuery, Result<PageSourcePagination<GetModeratorsEmailResponse>>>
    {

        private readonly IUnitOfWork _unitOfWork;
        public GetModeratorsEmailQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PageSourcePagination<GetModeratorsEmailResponse>>>
            Handle(GetModeratorsEmailQuery query, CancellationToken cancellationToken)
        {
            Expression<Func<Domain.Entities.Moderator, bool>> filter = m => true;
            if (query.isUsed != null)
            {
                filter = m => m.IsUsed == query.isUsed;
            }

            var moderators = await _unitOfWork.Moderators.GetAllPaginatedEnhancedAsync(
                filter: filter,
                selector: m => new GetModeratorsEmailResponse
                {
                    Id = m.Id,
                    Email = m.User.Email!
                },
                pageNumber: query.pageNumber,
                pageSize: 20);

            return Result<PageSourcePagination<GetModeratorsEmailResponse>>
                .Success(moderators, "Paginated Moderators' emails Retrieved Successfully");
        }
    }
}
