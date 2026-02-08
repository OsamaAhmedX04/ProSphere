using MediatR;
using ProSphere.Domain.Constants;
using ProSphere.Features.Verification.Queries.GetIdentityVerificationById;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProSphere.Features.Verification.Queries.GetFinancialInvestorVerificationById
{
    public class GetFinancialInvestorVerificationByIdQueryHandler :
        IRequestHandler<GetFinancialInvestorVerificationByIdQuery, Result<GetFinancialInvestorVerificationByIdResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFinancialInvestorVerificationByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetFinancialInvestorVerificationByIdResponse>> 
            Handle(GetFinancialInvestorVerificationByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.FinancialVerifications.GetEnhancedAsync(
               filter: v => v.Id == query.financialDocumentId,
               selector: v => new GetFinancialInvestorVerificationByIdResponse
               {
                   FinancialDocumentId = query.financialDocumentId,
                   UserId = v.InvestorId,
                   UserName = v.Investor.UserName,
                   DocumentType = v.DocumentType,
                   DocumentURL = SupabaseConstants.PrefixSupaURL + v.DocumentURL,
                   status = v.status.ToString(),
                   CreatedAt = v.CreatedAt,
                   Notes = v.Notes
               }) ?? new GetFinancialInvestorVerificationByIdResponse();

            return Result<GetFinancialInvestorVerificationByIdResponse>.Success(result, "Financial Document Retrieved Successfully");
        }
    }
}
