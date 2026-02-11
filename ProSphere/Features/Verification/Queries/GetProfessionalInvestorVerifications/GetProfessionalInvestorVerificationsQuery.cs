using MediatR;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Verification.Queries.GetProfessionalInvestorVerifications
{
    public record GetProfessionalInvestorVerificationsQuery(int pageNumber, string? status = null)
        : IRequest<Result<PageSourcePagination<GetProfessionalInvestorVerificationsResponse>>>
    {
    }
}
