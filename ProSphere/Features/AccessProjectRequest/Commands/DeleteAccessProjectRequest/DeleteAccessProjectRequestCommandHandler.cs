using MediatR;
using ProSphere.Domain.Enums;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.AccessProjectRequest.Commands.DeleteAccessProjectRequest
{
    public class DeleteAccessProjectRequestCommandHandler : IRequestHandler<DeleteAccessProjectRequestCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAccessProjectRequestCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteAccessProjectRequestCommand command, CancellationToken cancellationToken)
        {
            var accessRequest = await _unitOfWork.ProjectsAccessRequests.FirstOrDefaultAsync(x => x.Id == command.requestId);
            if (accessRequest == null) return Result.Failure("Request Not Exist", StatusCodes.Status404NotFound);

            if (accessRequest.Status != Status.Pending)
                return Result.Failure($"Access Request Is Already {accessRequest.Status.ToString()}", StatusCodes.Status400BadRequest);

            _unitOfWork.ProjectsAccessRequests.Delete(accessRequest.Id);
            await _unitOfWork.CompleteAsync();

            return Result.Success("Access Request Deleted Successfully");
        }
    }
}
