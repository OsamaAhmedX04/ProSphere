using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Verification.Queries.GetProfessionalInvestorVerificationById
{
    public record GetProfessionalInvestorVerificationByIdQuery(Guid professionalDocumentId)
        : IRequest<Result<GetProfessionalInvestorVerificationByIdResponse>>
    {
    }
}
