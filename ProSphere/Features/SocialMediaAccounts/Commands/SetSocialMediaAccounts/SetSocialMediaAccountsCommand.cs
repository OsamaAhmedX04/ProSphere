using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.SocialMediaAccounts.Commands.SetSocialMediaAccounts
{
    public record SetSocialMediaAccountsCommand(string userId, SetSocialMediaAccountsRequest request) : IRequest<Result>
    {
    }
}
