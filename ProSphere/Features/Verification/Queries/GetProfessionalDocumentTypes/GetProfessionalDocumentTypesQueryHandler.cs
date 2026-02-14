using MediatR;
using ProSphere.Domain.Constants;

namespace ProSphere.Features.Verification.Queries.GetProfessionalDocumentTypes
{
    // still work on it
    public class GetProfessionalDocumentTypesQueryHandler : IRequestHandler<GetProfessionalDocumentTypesQuery, List<string>>
    {
        public Task<List<string>> Handle(GetProfessionalDocumentTypesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(ProfessionalType.Types);
        }
    }
}
