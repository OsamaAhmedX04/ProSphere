using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Admin.Commands.DeleteAdmin
{
    public record DeleteAdminCommand(string adminId) : IRequest<Result>
    {
    }
}
