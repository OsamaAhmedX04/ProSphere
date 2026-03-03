using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectModerating.Queries.GetUpdatedPendingProjectDetails
{
    public record GetUpdatedPendingProjectDetailsQuery(Guid projectId) : IRequest<Result<GetUpdatedPendingProjectDetailsResponse>>
    {
    }
}
