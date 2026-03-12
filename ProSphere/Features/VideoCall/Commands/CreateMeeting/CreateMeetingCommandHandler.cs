using MediatR;
using Microsoft.AspNetCore.Identity;
using ProSphere.Domain.Constants.RoleConstants;
using ProSphere.Domain.Entities;
using ProSphere.ExternalServices.Interfaces.VideoCall;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.VideoCall.Commands.CreateMeeting
{
    public class CreateMeetingCommandHandler : IRequestHandler<CreateMeetingCommand, Result<CreateMeetingResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMeetingService _meetingService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateMeetingCommandHandler(IUnitOfWork unitOfWork, IMeetingService meetingService, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _meetingService = meetingService;
            _userManager = userManager;
        }

        public async Task<Result<CreateMeetingResponse>> Handle(CreateMeetingCommand command, CancellationToken cancellationToken)
        {
            var meeting = await _meetingService.CreateMeeting();
            var user = await _userManager.FindByIdAsync(command.firstUserId);
            var isfirstUserCreator = await _userManager.IsInRoleAsync(user!, Role.Creator);

            var meet = new Meeting
            {
                CreatorId = isfirstUserCreator ? command.firstUserId : command.secondUserId,
                InvestorId = isfirstUserCreator ? command.secondUserId : command.firstUserId,
                ZoomMeetingId = meeting.Id,
                JoinUrl = meeting.JoinUrl,
                StartUrl = meeting.StartUrl,
                Password = meeting.Password
            };

            await _unitOfWork.Meetings.AddAsync(meet);
            await _unitOfWork.CompleteAsync();

            return Result<CreateMeetingResponse>.Success(meeting, "Meeting Created Successfully");
        }
    }
}
