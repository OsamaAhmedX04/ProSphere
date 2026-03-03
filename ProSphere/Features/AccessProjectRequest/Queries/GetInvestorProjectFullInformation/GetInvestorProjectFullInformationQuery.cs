using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.AccessProjectRequest.Queries.GetInvestorProjectFullInformation
{
    public record GetInvestorProjectFullInformationQuery(string investorId, Guid projectId)
        : IRequest<Result<GetInvestorProjectFullInformationResponse>>
    {
    }
}
