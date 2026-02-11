using LinqKit;
using MediatR;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;
using System.Linq.Expressions;

namespace ProSphere.Features.Verification.Queries.GetFinancialInvestorVerifications
{
    public class GetFinancialInvestorVerificationsQueryHandler
        : IRequestHandler<GetFinancialInvestorVerificationsQuery, Result<PageSourcePagination<GetFinancialInvestorVerificationsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFinancialInvestorVerificationsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PageSourcePagination<GetFinancialInvestorVerificationsResponse>>>
            Handle(GetFinancialInvestorVerificationsQuery query, CancellationToken cancellationToken)
        {
            Expression<Func<FinancialVerification, bool>> filter = v => true;
            if (!string.IsNullOrEmpty(query.status))
            {
                var status = query.status.ToLower() switch
                {
                    "pending" => Status.Pending,
                    "approved" => Status.Approved,
                    "rejected" => Status.Rejected,
                    _ => Status.Pending
                };

                filter = filter.And(v => v.status == status);
            }

            var result = await _unitOfWork.FinancialVerifications.GetAllPaginatedEnhancedAsync(
                filter: filter,
                selector: v => new GetFinancialInvestorVerificationsResponse
                {
                    FinancialDocumentId = v.Id,
                    UserId = v.InvestorId,
                    UserName = v.Investor.UserName,
                    CreatedAt = v.CreatedAt,
                },
                pageNumber: query.pageNumber,
                pageSize: 20
                );

            return Result<PageSourcePagination<GetFinancialInvestorVerificationsResponse>>
                .Success(result, "Paginated Finacials Retrieved Successfully");
        }
    }
}
