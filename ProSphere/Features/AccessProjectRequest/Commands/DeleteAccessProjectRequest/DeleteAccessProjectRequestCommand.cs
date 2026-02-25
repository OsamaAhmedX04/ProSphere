using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.AccessProjectRequest.Commands.DeleteAccessProjectRequest
{
    public record DeleteAccessProjectRequestCommand(Guid requestId) : IRequest<Result>
    {
    }
}
