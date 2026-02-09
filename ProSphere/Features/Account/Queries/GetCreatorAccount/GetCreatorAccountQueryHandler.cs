using MediatR;
using ProSphere.Domain.Constants;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Account.Queries.GetCreatorAccount
{
    public class GetCreatorAccountQueryHandler : IRequestHandler<GetCreatorAccountQuery, Result<GetCreatorAccountResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCreatorAccountQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetCreatorAccountResponse>> Handle(GetCreatorAccountQuery query, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Creators.GetEnhancedAsync(
                filter: c => c.Id == query.userId,
                selector: c => new GetCreatorAccountResponse
                {
                    FirstName = c.User.FirstName,
                    LastName = c.User.LastName,
                    Gender = c.User.Gender.ToString(),
                    ImageProfileURL = SupabaseConstants.PrefixSupaURL + c.ImageProfileURL ?? null,
                    IsVerified = c.User.IsVerified
                });

            if (result is null)
                return Result<GetCreatorAccountResponse>.Failure("User Not Found", StatusCodes.Status404NotFound);

            return Result<GetCreatorAccountResponse>.Success(result, "Creator Account Retrieved Successfullt");
        }
    }
}
