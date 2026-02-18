using MediatR;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.CV.Queries.GetCVByUserId
{
    public class GetCVByUserIdQueryHandler : IRequestHandler<GetCVByUserIdQuery, Result<GetCVByUserIdResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCVByUserIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetCVByUserIdResponse>> Handle(GetCVByUserIdQuery query, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork.Creators.GetEnhancedAsync(
                filter: c => c.Id == query.UserId,
                selector: c => new GetCVByUserIdResponse
                {
                    CVURL = c.CVURL
                });

            if (response is null)
                return Result<GetCVByUserIdResponse>.Failure("User Not Found", StatusCodes.Status404NotFound);

            return Result<GetCVByUserIdResponse>.Success(response, "CV Retrieved Successfully");
        }
    }
}
