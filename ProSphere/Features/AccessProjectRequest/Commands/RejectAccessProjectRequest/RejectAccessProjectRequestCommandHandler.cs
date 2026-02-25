using MediatR;
using ProSphere.Domain.Enums;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.AccessProjectRequest.Commands.RejectAccessProjectRequest
{
    public class RejectAccessProjectRequestCommandHandler : IRequestHandler<RejectAccessProjectRequestCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RejectAccessProjectRequestCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(RejectAccessProjectRequestCommand command, CancellationToken cancellationToken)
        {
            var accessRequest = await _unitOfWork.ProjectsAccessRequests.FirstOrDefaultAsync(x => x.Id == command.requestId);
            if (accessRequest == null) return Result.Failure("Request Not Exist", StatusCodes.Status404NotFound);

            if (accessRequest.Status != Status.Pending)
                return Result.Failure($"Access Request Is Already {accessRequest.Status.ToString()}", StatusCodes.Status400BadRequest);

            accessRequest.Status = Status.Rejected;
            await _unitOfWork.CompleteAsync();

            return Result.Success("Request Is Rejected Successfully");
        }
    }
}
