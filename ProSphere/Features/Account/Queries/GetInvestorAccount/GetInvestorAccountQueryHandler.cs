using MediatR;
using ProSphere.Domain.Constants;
using ProSphere.Domain.Enums;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Account.Queries.GetInvestorAccount
{
    public class GetInvestorAccountQueryHandler : IRequestHandler<GetInvestorAccountQuery, Result<GetInvestorAccountResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetInvestorAccountQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetInvestorAccountResponse>> Handle(GetInvestorAccountQuery query, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Investors.GetEnhancedAsync(
                filter: i => i.Id == query.userId,
                selector: i => new GetInvestorAccountResponse
                {
                    FirstName = i.User.FirstName,
                    LastName = i.User.LastName,
                    Gender = i.User.Gender.ToString(),
                    ImageProfileURL = SupabaseConstants.PrefixSupaURL + i.ImageProfileURL ?? null,
                    IsVerified = i.User.IsVerified,
                    IsFinancail = i.InvestorLevel.ToString() == InvestorLevel.Professional.ToString()
                                  ||
                                  i.InvestorLevel.ToString() == InvestorLevel.Financial.ToString(),

                    IsProfessional = i.InvestorLevel.ToString() == InvestorLevel.Professional.ToString()
                });

            if (result is null)
                return Result<GetInvestorAccountResponse>.Failure("User Not Found", StatusCodes.Status404NotFound);

            return Result<GetInvestorAccountResponse>.Success(result, "Investor Account Retrieved Successfullt");
        }
    }
}
