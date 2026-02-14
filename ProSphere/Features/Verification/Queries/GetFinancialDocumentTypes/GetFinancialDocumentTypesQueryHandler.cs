using MediatR;
using ProSphere.Domain.Constants;

namespace ProSphere.Features.Verification.Queries.GetFinancialDocumentTypes
{
    // still work on it
    public class GetFinancialDocumentTypesQueryHandler : IRequestHandler<GetFinancialDocumentTypesQuery, List<string>>
    {
        public Task<List<string>> Handle(GetFinancialDocumentTypesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(FinancialType.Types);
        }
    }
}
