using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Reports.Commands.SendReportOnUser
{
    public record SendReportOnUserCommand(string reporterId, string targetUserId, SendReportOnUserRequest request) : IRequest<Result>
    {
    }
}
