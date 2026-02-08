using Azure.Core;
using LinqKit;
using MediatR;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;
using System.Linq.Expressions;

namespace ProSphere.Features.Verification.Queries.GetIdentityVerifications
{
    public class GetIdentityVerificationQueryHandler :
        IRequestHandler<GetIdentityVerificationQuery, Result<PageSourcePagination<GetIdentityVerificationResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetIdentityVerificationQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PageSourcePagination<GetIdentityVerificationResponse>>> 
            Handle(GetIdentityVerificationQuery query, CancellationToken cancellationToken)
        {
            Expression<Func<IdentityVerification, bool>> filter = v => true;
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

            var result = await _unitOfWork.IdentityVerifications.GetAllPaginatedEnhancedAsync(
                filter: filter,
                selector: v => new GetIdentityVerificationResponse
                {
                    IdentityDocumentId = v.Id,
                    UserId = v.UserId,
                    UserName = v.User.FirstName + " " + v.User.LastName,
                    CreatedAt = v.CreatedAt,
                },
                pageNumber: query.pageNumber,
                pageSize: 20
                );
            
            return Result<PageSourcePagination<GetIdentityVerificationResponse>>
                .Success(result, "Paginated Identites Retrieved Successfully");

        }
    }
}
