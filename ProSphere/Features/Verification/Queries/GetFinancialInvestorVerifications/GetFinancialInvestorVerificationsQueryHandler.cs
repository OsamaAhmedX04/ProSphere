using LinqKit;
using MediatR;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.Features.Verification.Queries.GetIdentityVerifications;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProSphere.Features.Verification.Queries.GetFinancialInvestorVerifications
{
    public class GetFinancialInvestorVerificationQueryHandler
        : IRequestHandler<GetFinancialInvestorVerificationQuery, Result<PageSourcePagination<GetFinancialInvestorVerificationResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFinancialInvestorVerificationQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PageSourcePagination<GetFinancialInvestorVerificationResponse>>> 
            Handle(GetFinancialInvestorVerificationQuery query, CancellationToken cancellationToken)
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
                selector: v => new GetFinancialInvestorVerificationResponse
                {
                    FinancialDocumentId = v.Id,
                    UserId = v.InvestorId,
                    UserName = v.Investor.UserName,
                    CreatedAt = v.CreatedAt,
                },
                pageNumber: query.pageNumber,
                pageSize: 20
                );

            return Result<PageSourcePagination<GetFinancialInvestorVerificationResponse>>
                .Success(result, "Paginated Finacials Retrieved Successfully");
        }
    }
}
