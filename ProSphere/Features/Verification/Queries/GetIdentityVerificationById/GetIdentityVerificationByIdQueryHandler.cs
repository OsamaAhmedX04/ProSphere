using MediatR;
using ProSphere.Domain.Constants;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Verification.Queries.GetIdentityVerificationById
{
    public class GetIdentityVerificationByIdQueryHandler :
        IRequestHandler<GetIdentityVerificationByIdQuery, Result<GetIdentityVerificationByIdResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetIdentityVerificationByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetIdentityVerificationByIdResponse>> Handle(GetIdentityVerificationByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.IdentityVerifications.GetEnhancedAsync(
                filter: v => v.Id == query.identityDocumentId,
                selector: v => new GetIdentityVerificationByIdResponse
                {
                    IdentityDocumentId = query.identityDocumentId,
                    IdBackImageURL = SupabaseConstants.PrefixSupaURL + v.IdBackImageURL,
                    IdFrontImageURL = SupabaseConstants.PrefixSupaURL + v.IdFrontImageURL,
                    SelfieWithIdURL = SupabaseConstants.PrefixSupaURL + v.SelfieWithIdURL,
                    UserId = v.UserId,
                    UserName = v.User.FirstName + " " + v.User.LastName,
                    status = v.status.ToString(),
                    CreatedAt = v.CreatedAt
                }) ?? new GetIdentityVerificationByIdResponse();

            return Result<GetIdentityVerificationByIdResponse>.Success(result, "Identity Document Retrieved Successfully");
        }
    }
}
