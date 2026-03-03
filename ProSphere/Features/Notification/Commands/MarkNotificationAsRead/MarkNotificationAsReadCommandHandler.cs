using MediatR;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Notification.Commands.MarkNotificationAsRead
{
    public class MarkNotificationAsReadCommandHandler : IRequestHandler<MarkNotificationAsReadCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public MarkNotificationAsReadCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(MarkNotificationAsReadCommand command, CancellationToken cancellationToken)
        {
            var notification = await _unitOfWork.Notifications
                .FirstOrDefaultAsync(n => n.Id == command.notificationId && n.UserId == command.userId);

            if (notification == null) return Result.Failure("Notification Not Exist", StatusCodes.Status404NotFound);

            notification.IsSeen = true;
            notification.SeenAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();

            return Result.Success("Notification Read Successfully");
        }
    }
}
