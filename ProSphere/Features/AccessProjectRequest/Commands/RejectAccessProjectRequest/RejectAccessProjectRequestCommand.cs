using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.AccessProjectRequest.Commands.RejectAccessProjectRequest
{
    public record RejectAccessProjectRequestCommand(Guid requestId) : IRequest<Result>
    {
    }
}
