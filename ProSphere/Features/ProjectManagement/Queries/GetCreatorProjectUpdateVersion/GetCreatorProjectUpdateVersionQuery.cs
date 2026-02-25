using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectManagement.Queries.GetCreatorProjectUpdateVersion
{
    public record GetCreatorProjectUpdateVersionQuery(string creatorId, Guid projectId)
        : IRequest<Result<GetCreatorProjectUpdateVersionResponse>>
    {
    }
}
