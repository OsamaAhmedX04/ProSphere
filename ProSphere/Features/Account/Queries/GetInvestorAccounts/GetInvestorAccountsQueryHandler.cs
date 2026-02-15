using LinqKit;
using MediatR;
using ProSphere.Domain.Constants.FileConstants;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.Features.Account.Queries.GetInvestorAccount;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;
using System.Linq.Expressions;

namespace ProSphere.Features.Account.Queries.GetInvestorAccounts
{
    public class GetInvestorAccountsQueryHandler 
        : IRequestHandler<GetInvestorAccountsQuery, Result<PageSourcePagination<GetInvestorAccountsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetInvestorAccountsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PageSourcePagination<GetInvestorAccountsResponse>>> 
            Handle(GetInvestorAccountsQuery query, CancellationToken cancellationToken)
        {
            Expression<Func<Investor, bool>> filter = c => true;

            if (!string.IsNullOrEmpty(query.userName))
            {
                filter = filter.And(c => c.UserName.Contains(query.userName));
            }

            var result = await _unitOfWork.Investors.GetAllPaginatedEnhancedAsync(
                filter: filter,
                selector: i => new GetInvestorAccountsResponse
                {
                    UserName = i.UserName,
                    ImageProfileURL = SupabaseConstants.PrefixSupaURL + i.ImageProfileURL ?? null,
                    HeadLine = i.HeadLine,
                    IsVerified = i.User.IsVerified,
                    IsFinancail = i.InvestorLevel == InvestorLevel.Professional
                                  ||
                                  i.InvestorLevel == InvestorLevel.Financial,

                    IsProfessional = i.InvestorLevel == InvestorLevel.Professional
                });
            return Result<PageSourcePagination<GetInvestorAccountsResponse>>.Success(result, "Paginated Investors Retreived Successfully");
        }
    }
}
