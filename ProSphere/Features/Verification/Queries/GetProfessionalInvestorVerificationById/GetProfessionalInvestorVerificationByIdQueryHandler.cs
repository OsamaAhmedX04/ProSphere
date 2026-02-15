using MediatR;
using ProSphere.Domain.Constants.FileConstants;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Verification.Queries.GetProfessionalInvestorVerificationById
{
    public class GetProfessionalInvestorVerificationByIdQueryHandler
        : IRequestHandler<GetProfessionalInvestorVerificationByIdQuery, Result<GetProfessionalInvestorVerificationByIdResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProfessionalInvestorVerificationByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetProfessionalInvestorVerificationByIdResponse>>
            Handle(GetProfessionalInvestorVerificationByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.ProfessionalVerifications.GetEnhancedAsync(
               filter: v => v.Id == query.professionalDocumentId,
               selector: v => new GetProfessionalInvestorVerificationByIdResponse
               {
                   ProfessionalDocumentId = query.professionalDocumentId,
                   UserId = v.InvestorId,
                   UserName = v.Investor.UserName,
                   DocumentType = v.DocumentType.Name,
                   DocumentURL = SupabaseConstants.PrefixSupaURL + v.DocumentURL,
                   status = v.status.ToString(),
                   CreatedAt = v.CreatedAt,
                   Notes = v.Notes
               }) ?? new GetProfessionalInvestorVerificationByIdResponse();

            return Result<GetProfessionalInvestorVerificationByIdResponse>.Success(result, "Professional Document Retrieved Successfully");
        }
    }
}
