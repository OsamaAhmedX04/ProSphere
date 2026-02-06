using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Account.Commands.DeleteAccount
{
    public record DeleteAccountCommand(string userId, string otp) : IRequest<Result>
    {
    }
}
