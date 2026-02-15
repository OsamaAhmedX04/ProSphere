using Hangfire;
using LinqKit;
using MediatR;
using ProSphere.Domain.Entities;
using ProSphere.Jobs.Search.SearchSaver;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;
using System.Linq.Expressions;

namespace ProSphere.Features.Search.Queries.SearchForCreators
{
    public class SearchForCreatorsQueryHandler : IRequestHandler<SearchForCreatorsQuery, Result<PageSourcePagination<SearchForCreatorsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SearchForCreatorsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PageSourcePagination<SearchForCreatorsResponse>>>
            Handle(SearchForCreatorsQuery query, CancellationToken cancellationToken)
        {
            Expression<Func<Creator, bool>> filter = c => true;

            if (!string.IsNullOrEmpty(query.userName))
                filter = filter.And(c => c.UserName.Contains(query.userName));

            if (query.verified.HasValue)
                filter = filter.And(c => c.User.IsVerified == query.verified.Value);

            var result = await _unitOfWork.Creators.GetAllPaginatedEnhancedAsync(
                filter: filter,
                selector: c => new SearchForCreatorsResponse
                {
                    Id = c.Id,
                    UserName = c.UserName,
                    ImageProfileURL = c.ImageProfileURL,
                    HeadLine = c.HeadLine,
                    IsVerified = c.User.IsVerified
                },
                pageNumber: query.pageNumber,
                pageSize: 20
                );

            if (query.userId != null)
            {
                BackgroundJob.Enqueue<ISearchSaver>(
                     service => service.SaveSearchAtCreatorHistory(
                         query.userId, query.userName, query.verified
                  ));
            }


            return Result<PageSourcePagination<SearchForCreatorsResponse>>
                .Success(result, "Paginated Search For Creators Retreived Successfully");
        }
    }
}
