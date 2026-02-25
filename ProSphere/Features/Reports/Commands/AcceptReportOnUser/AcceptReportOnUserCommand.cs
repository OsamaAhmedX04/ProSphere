using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Reports.Commands.AcceptReportOnUser
{
    public record AcceptReportOnUserCommand(string moderatorId, Guid reportId, int numberOfBanDays) : IRequest<Result>
    {
    }
}
