using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectManagement.Queries.GetCreatorProjectData
{
    public record GetCreatorProjectDataQuery(Guid projectId) : IRequest<Result<GetCreatorProjectDataResponse>>
    {
    }
}
