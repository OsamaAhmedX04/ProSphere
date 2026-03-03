using MediatR;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Notification.Commands.DeleteNotification
{
    public class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteNotificationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteNotificationCommand command, CancellationToken cancellationToken)
        {
            var isNotificationExist = await _unitOfWork.Notifications
                .AnyAsync(n => n.UserId == command.userId && n.Id == command.notificationId);

            if (!isNotificationExist) return Result.Failure("Notification Not Exist", StatusCodes.Status404NotFound);

            _unitOfWork.Notifications.Delete(command.notificationId);

            await _unitOfWork.CompleteAsync();

            return Result.Success("Notification Deleted Successfully");
        }
    }
}
