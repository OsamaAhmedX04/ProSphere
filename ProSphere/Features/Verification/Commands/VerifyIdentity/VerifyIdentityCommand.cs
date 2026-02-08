using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Verification.Commands.VerifyIdentity
{
    public record VerifyIdentityCommand(string userId, VerifyIdentityRequest request) : IRequest<Result>
    {
    }
}
