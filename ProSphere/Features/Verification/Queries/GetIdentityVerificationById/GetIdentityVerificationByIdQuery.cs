using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Verification.Queries.GetIdentityVerificationById
{
    public record GetIdentityVerificationByIdQuery(Guid identityDocumentId) : IRequest<Result<GetIdentityVerificationByIdResponse>>
    {
    }
}
