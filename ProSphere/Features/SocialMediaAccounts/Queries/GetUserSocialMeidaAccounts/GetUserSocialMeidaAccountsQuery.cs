using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.SocialMediaAccounts.Queries.GetUserSocialMeidaAccounts
{
    public record GetUserSocialMeidaAccountsQuery(string userId) : IRequest<Result<GetUserSocialMeidaAccountsResponse>>
    {
    }
}
