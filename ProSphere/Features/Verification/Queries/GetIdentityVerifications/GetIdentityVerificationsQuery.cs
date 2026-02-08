using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Verification.Queries.GetIdentityVerifications
{
    public record GetIdentityVerificationQuery(int pageNumber, string? status = null)
        : IRequest<Result<PageSourcePagination<GetIdentityVerificationResponse>>>
    {
    }
}
