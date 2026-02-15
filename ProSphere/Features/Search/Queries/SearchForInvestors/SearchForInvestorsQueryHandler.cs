using Hangfire;
using LinqKit;
using MediatR;
using ProSphere.Domain.Constants.SearchConstants;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.Features.Account.Queries.GetInvestorAccounts;
using ProSphere.Jobs.Search.SearchSaver;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;
using System.Linq.Expressions;

namespace ProSphere.Features.Search.Queries.SearchForInvestors
{
    public class SearchForInvestorsQueryHandler
        : IRequestHandler<SearchForInvestorsQuery, Result<PageSourcePagination<GetInvestorAccountsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SearchForInvestorsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PageSourcePagination<GetInvestorAccountsResponse>>> 
            Handle(SearchForInvestorsQuery query, CancellationToken cancellationToken)
        {
            Expression<Func<Investor, bool>> filter = i => true;

            if (!string.IsNullOrEmpty(query.userName))
                filter = filter.And(i => i.UserName.Contains(query.userName));

            if (query.verified.HasValue)
                filter = filter.And(i => i.User.IsVerified == query.verified.Value);

            if (query.financial.HasValue && query.financial == true)
                filter = filter.And(i => i.InvestorLevel == InvestorLevel.Financial);

            if (query.professional.HasValue && query.professional == true)
                filter = filter.And(i => i.InvestorLevel == InvestorLevel.Professional);

            var result = await _unitOfWork.Investors.GetAllPaginatedEnhancedAsync(
                filter: filter,
                selector: i => new GetInvestorAccountsResponse
                    {
                        UserName = i.UserName,
                        ImageProfileURL = i.ImageProfileURL,
                        HeadLine = i.HeadLine,
                        IsVerified = i.User.IsVerified,
                        IsFinancail = i.InvestorLevel == InvestorLevel.Financial || i.InvestorLevel == InvestorLevel.Professional,
                        IsProfessional = i.InvestorLevel == InvestorLevel.Professional
                    },
                pageNumber: query.pageNumber,
                pageSize: 20
            );
            if (query.userId != null)
            {
                BackgroundJob.Enqueue<ISearchSaver>(
                    service => service.SaveSearchAtInvestorHistory(
                        query.userId, query.userName, query.verified, query.financial, query.professional
                ));
            }


            return Result<PageSourcePagination<GetInvestorAccountsResponse>>.Success(result, "Paginated Search For Investors Retreived Successfully");
        }
    }
}
