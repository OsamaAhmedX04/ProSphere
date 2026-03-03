using MediatR;
using Microsoft.EntityFrameworkCore;
using ProSphere.Domain.Enums;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;
using System.Runtime.InteropServices;

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
            accessRequest.RespondedAt = DateTime.UtcNow;
            await _unitOfWork.ProjectsAccessRequests
                .GetAllAsIQueryable()
                .Where(p => p.ProjectId == accessRequest.ProjectId && p.Status == Status.Pending)
                .ExecuteUpdateAsync(setters => setters.SetProperty(p => p.Status, Status.Rejected));

            var project = await _unitOfWork.Projects.FirstOrDefaultAsync(p => p.Id == accessRequest.ProjectId);
            if(project is not null) project.IsInvested = true;

            await _unitOfWork.CompleteAsync();

            return Result.Success("Access Request Accepted Successfully");
        }
    }
}
