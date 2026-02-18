using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.CV.Commands.UploadCV
{
    public record UploadCVCommand(string UserId, UploadCVRequest Request) : IRequest<Result>
    {
    }
}
