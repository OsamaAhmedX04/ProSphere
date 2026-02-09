using MediatR;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Account.Queries.GetAdminAccount
{
    public class GetAdminAccountQueryHandler : IRequestHandler<GetAdminAccountQuery, Result<GetAdminAccountResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAdminAccountQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetAdminAccountResponse>> Handle(GetAdminAccountQuery query, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Admins.GetEnhancedAsync(
                filter: a => a.Id == query.userId,
                selector: a => new GetAdminAccountResponse
                {
                    FirstName = a.User.FirstName,
                    LastName = a.User.LastName,
                    Gender = a.User.Gender.ToString(),
                    IsSuperAdmin = a.IsSuperAdmin,
                });

            if (result is null)
                return Result<GetAdminAccountResponse>.Failure("User Not Found", StatusCodes.Status404NotFound);

            return Result<GetAdminAccountResponse>.Success(result, "Admin Account Retrieved Successfully");
        }
    }
}
