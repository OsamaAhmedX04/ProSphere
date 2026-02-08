using LinqKit;
using MediatR;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;
using System.Linq.Expressions;

namespace ProSphere.Features.Verification.Queries.GetProfessionalInvestorVerifications
{
    public class GetProfessionalInvestorVerificationQueryHandler
        : IRequestHandler<GetProfessionalInvestorVerificationQuery, Result<PageSourcePagination<GetProfessionalInvestorVerificationResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProfessionalInvestorVerificationQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PageSourcePagination<GetProfessionalInvestorVerificationResponse>>>
            Handle(GetProfessionalInvestorVerificationQuery query, CancellationToken cancellationToken)
        {
            Expression<Func<ProfessionalVerification, bool>> filter = v => true;
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

            var result = await _unitOfWork.ProfessionalVerifications.GetAllPaginatedEnhancedAsync(
                filter: filter,
                selector: v => new GetProfessionalInvestorVerificationResponse
                {
                    ProfessionalDocumentId = v.Id,
                    UserId = v.InvestorId,
                    UserName = v.Investor.UserName,
                    CreatedAt = v.CreatedAt,
                },
                pageNumber: query.pageNumber,
                pageSize: 20
                );

            return Result<PageSourcePagination<GetProfessionalInvestorVerificationResponse>>
                .Success(result, "Paginated Professionals Retrieved Successfully");
        }
    }
}
