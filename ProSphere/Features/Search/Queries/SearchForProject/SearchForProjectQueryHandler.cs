using Hangfire;
using LinqKit;
using MediatR;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.Features.Search.Queries.SearchForCreators;
using ProSphere.Jobs.Search.SearchSaver;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;
using System.Linq.Expressions;

namespace ProSphere.Features.Search.Queries.SearchForProject
{
    public class SearchForProjectQueryHandler : IRequestHandler<SearchForProjectQuery, Result<PageSourcePagination<SearchForProjectResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SearchForProjectQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PageSourcePagination<SearchForProjectResponse>>>
            Handle(SearchForProjectQuery query, CancellationToken cancellationToken)
        {
            Expression<Func<Project, bool>> filter = p => p.Status == Status.Approved && !p.IsBlocked && !p.IsBlockedDueToBannedUser;

            if (!string.IsNullOrEmpty(query.projectName))
                filter = filter.And(c => c.Title.Contains(query.projectName) || c.ShortDescription.Contains(query.projectName));

            var result = await _unitOfWork.Projects.GetAllPaginatedEnhancedAsync(
                filter: filter,
                selector: p => new SearchForProjectResponse
                {
                    ProjectId = p.Id,
                    CreatorId = p.CreatorId,
                    ShortDescription = p.ShortDescription,
                    Title = p.Title,
                    EquityPercentage = p.EquityPercentage,
                    IsInvested = p.IsInvested,
                    Market = p.Market,
                    NeededInvestment = p.NeededInvestment,
                    Problem = p.Problem,
                    Images = p.Images.Select(pi => pi.ImageUrl).ToList(),
                    SolutionSummary = p.SolutionSummary,
                    UpdatedAt = p.UpdatedAt,
                    CreatedAt = p.CreatedAt,
                },
                pageNumber: query.pageNumber,
                pageSize: 20
                );

            if (query.userId != null)
            {
                BackgroundJob.Enqueue<ISearchSaver>(
                     service => service.SaveSearchAtProjectHistory(
                         query.userId, query.projectName
                  ));
            }


            return Result<PageSourcePagination<SearchForProjectResponse>>
                .Success(result, "Paginated Search For Projects Retreived Successfully");
        }
    }
}
