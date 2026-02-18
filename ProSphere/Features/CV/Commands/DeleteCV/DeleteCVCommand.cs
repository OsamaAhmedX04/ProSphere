using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.CV.Commands.DeleteCV
{
    public record DeleteCVCommand(string UserId) : IRequest<Result>
    {
    }
}
