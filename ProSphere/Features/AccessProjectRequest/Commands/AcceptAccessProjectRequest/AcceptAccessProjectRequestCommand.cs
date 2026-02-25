using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.AccessProjectRequest.Commands.AcceptAccessProjectRequest
{
    public record AcceptAccessProjectRequestCommand(Guid requestId) : IRequest<Result>
    {
    }
}
