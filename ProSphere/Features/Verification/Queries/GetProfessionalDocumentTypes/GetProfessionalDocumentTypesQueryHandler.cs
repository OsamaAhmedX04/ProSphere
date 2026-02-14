using MediatR;
using ProSphere.Domain.Constants;
using ProSphere.Features.Verification.Queries.GetFinancialDocumentTypes;
using ProSphere.RepositoryManager.Interfaces;

namespace ProSphere.Features.Verification.Queries.GetProfessionalDocumentTypes
{
    // still work on it
    public class GetProfessionalDocumentTypesQueryHandler 
        : IRequestHandler<GetProfessionalDocumentTypesQuery, List<GetProfessionalDocumentTypesResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProfessionalDocumentTypesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetProfessionalDocumentTypesResponse>> 
            Handle(GetProfessionalDocumentTypesQuery query, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.ProfessionalDocumentTypes.GetAllAsyncEnhanced(
                selector: x => new GetProfessionalDocumentTypesResponse
                {
                    DocumentTypeId = x.Id,
                    Name = x.Name
                });

            return result;
        }
    }
}
