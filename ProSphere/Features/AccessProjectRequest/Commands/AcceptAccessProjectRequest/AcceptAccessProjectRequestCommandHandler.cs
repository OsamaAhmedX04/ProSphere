using MediatR;
using Microsoft.EntityFrameworkCore;
using ProSphere.Domain.Enums;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.AccessProjectRequest.Commands.AcceptAccessProjectRequest
{
    public class AcceptAccessProjectRequestCommandHandler : IRequestHandler<AcceptAccessProjectRequestCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AcceptAccessProjectRequestCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AcceptAccessProjectRequestCommand command, CancellationToken cancellationToken)
        {
            var accessRequest = await _unitOfWork.ProjectsAccessRequests.FirstOrDefaultAsync(x => x.Id == command.requestId);
            if (accessRequest == null) return Result.Failure("Request Not Exist", StatusCodes.Status404NotFound);

            if (accessRequest.Status != Status.Pending)
                return Result.Failure($"Access Request Is Already {accessRequest.Status.ToString()}", StatusCodes.Status400BadRequest);

            accessRequest.Status = Status.Approved;
            await _unitOfWork.ProjectsAccessRequests
                .GetAllAsIQueryable()
                .Where(p => p.ProjectId == accessRequest.ProjectId && p.Status == Status.Pending)
                .ExecuteUpdateAsync(setters => setters.SetProperty(p => p.Status, Status.Rejected));

            await _unitOfWork.CompleteAsync();

            return Result.Success("Access Request Accepted Successfully");
        }
    }
}
