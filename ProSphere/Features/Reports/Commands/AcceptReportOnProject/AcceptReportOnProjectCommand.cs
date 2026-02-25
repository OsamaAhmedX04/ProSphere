using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Reports.Commands.AcceptReportOnProject
{
    public record AcceptReportOnProjectCommand(string moderatorId, Guid reportId) : IRequest<Result>
    {
    }
}
