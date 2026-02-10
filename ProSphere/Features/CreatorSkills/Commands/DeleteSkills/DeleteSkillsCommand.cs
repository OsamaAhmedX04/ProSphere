using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.CreatorSkills.Commands.DeleteSkills
{
    public record DeleteSkillsCommand(string creatorId, DeleteSkillsRequest request) : IRequest<Result>
    {
    }
}
