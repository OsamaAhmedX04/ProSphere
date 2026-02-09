using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Admin.Commands.CreateAdmin
{
    public record CreateAdminCommand(CreateAdminRequest request) : IRequest<Result<CreateAdminResponse>>
    {
    }
}
