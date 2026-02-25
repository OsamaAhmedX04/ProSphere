using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectModerating.Queries.GetPendingProjectDetails
{
    public record GetPendingProjectDetailsQuery(Guid projectId) : IRequest<Result<GetPendingProjectDetailsResponse>>
    {
    }
}
