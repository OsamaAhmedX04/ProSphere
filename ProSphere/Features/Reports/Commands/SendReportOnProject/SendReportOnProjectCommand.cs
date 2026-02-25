using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Reports.Commands.SendReportOnProject
{
    public record SendReportOnProjectCommand(string reporterId, Guid projectId, SendReportOnProjectRequest request) 
        : IRequest<Result>
    {
    }
}
