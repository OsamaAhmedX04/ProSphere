using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectManagement.Queries.GetCreatorProjectData
{
    public record GetCreatorProjectDataQuery(string creatorId, Guid projectId) : IRequest<Result<GetCreatorProjectDataResponse>>
    {
    }
}
