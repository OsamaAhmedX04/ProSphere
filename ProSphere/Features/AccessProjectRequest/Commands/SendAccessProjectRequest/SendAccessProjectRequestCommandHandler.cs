using FluentValidation;
using MediatR;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.Extensions;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.AccessProjectRequest.Commands.SendAccessProjectRequest
{
    public class SendAccessProjectRequestCommandHandler : IRequestHandler<SendAccessProjectRequestCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<SendAccessProjectRequestRequest> _validator;

        public SendAccessProjectRequestCommandHandler(IUnitOfWork unitOfWork, IValidator<SendAccessProjectRequestRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result> Handle(SendAccessProjectRequestCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command.request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ConvertErrorsToDictionary();
                return Result.ValidationFailure(errors);
            }

            var isInvestorExist = await _unitOfWork.Investors.IsExistAsync(command.investorId);
            if (!isInvestorExist) return Result.Failure("Investor Not Exist", StatusCodes.Status404NotFound);

            var isProjectExist = await _unitOfWork.Projects.IsExistAsync(command.projectId);
            if (!isProjectExist) return Result.Failure("Project Not Exist", StatusCodes.Status404NotFound);

            var accessrequest = await _unitOfWork.ProjectsAccessRequests
                .FirstOrDefaultAsync(p => p.ProjectId == command.projectId && p.InvestorId == command.investorId);
            if(accessrequest is not null)
                return Result.Failure($"Your Request Is Already {accessrequest.Status.ToString()}, Can Not Send More Requests For This Project", StatusCodes.Status400BadRequest);

            var request = new ProjectAccessRequest
            {
                ProjectId = command.projectId,
                InvestorId = command.investorId,
                Message = command.request.Message,
                Status = Status.Pending,
            };

            await _unitOfWork.ProjectsAccessRequests.AddAsync(request);
            await _unitOfWork.CompleteAsync();

            return Result.Success("Request Has Been Sent Successfully");
        }
    }
}
