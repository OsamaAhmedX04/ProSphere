using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.AccessProjectRequest.Commands.SendAccessProjectRequest
{
    public record SendAccessProjectRequestCommand(string investorId, Guid projectId, SendAccessProjectRequestRequest request)
        :IRequest<Result>
    {
    }
}
