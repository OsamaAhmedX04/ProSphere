using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Verification.Queries.GetProfessionalInvestorVerifications
{
    public record GetProfessionalInvestorVerificationQuery(int pageNumber, string? status = null)
        : IRequest<Result<PageSourcePagination<GetProfessionalInvestorVerificationResponse>>>
    {
    }
}
