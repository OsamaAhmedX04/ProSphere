using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Reports.Commands.RejectReportOnProject
{
    public record RejectReportOnProjectCommand(string moderatorId, Guid reportId) : IRequest<Result>
    {
    }
}
