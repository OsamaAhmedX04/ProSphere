using MediatR;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Notification.Commands.DeleteAllNotifications
{
    public class DeleteAllNotificationsCommandHandler : IRequestHandler<DeleteAllNotificationsCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAllNotificationsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteAllNotificationsCommand command, CancellationToken cancellationToken)
        {
            await _unitOfWork.Notifications.BulkDeleteAsync(n => n.UserId == command.userId);
            return Result.Success("All Notifications Deleted Successfully");
        }
    }
}
