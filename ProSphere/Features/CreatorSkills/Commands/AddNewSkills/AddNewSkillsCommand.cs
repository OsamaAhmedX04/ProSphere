using MediatR;
using ProSphere.ResultResponse;

namespace ProSphere.Features.CreatorSkills.Commands.AddNewSkills
{
    public record AddNewSkillsCommand(string CreatorId, AddNewSkillsRequest request) : IRequest<Result>
    {
    }
}
