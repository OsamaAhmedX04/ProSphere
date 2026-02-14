using MediatR;
using ProSphere.RepositoryManager.Interfaces;

namespace ProSphere.Features.Verification.Queries.GetFinancialDocumentTypes
{
    // still work on it
    public class GetFinancialDocumentTypesQueryHandler : IRequestHandler<GetFinancialDocumentTypesQuery, List<GetFinancialDocumentTypesResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFinancialDocumentTypesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetFinancialDocumentTypesResponse>>
            Handle(GetFinancialDocumentTypesQuery query, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.FinancialDocumentTypes.GetAllAsyncEnhanced(
                selector: x => new GetFinancialDocumentTypesResponse
                {
                    DocumentTypeId = x.Id,
                    Name = x.Name
                });

            return result;
        }
    }
}
