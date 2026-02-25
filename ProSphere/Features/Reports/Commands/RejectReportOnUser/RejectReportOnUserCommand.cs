using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Reports.Commands.RejectReportOnUser
{
    public record RejectReportOnUserCommand(string moderatorId, Guid reportId) : IRequest<Result>
    {
    }
}
