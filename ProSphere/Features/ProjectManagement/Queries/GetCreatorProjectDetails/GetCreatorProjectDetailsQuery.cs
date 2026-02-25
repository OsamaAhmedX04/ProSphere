using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.ProjectManagement.Queries.GetCreatorProjectDetails
{
    public record GetCreatorProjectDetailsQuery(string creatorId, Guid projectId) : IRequest<Result<GetCreatorProjectDetailsResponse>>
    {
    }
}
