using MediatR;

namespace ProSphere.Features.Verification.Queries.GetProfessionalDocumentTypes
{
    public record GetProfessionalDocumentTypesQuery : IRequest<List<GetProfessionalDocumentTypesResponse>>
    {
    }
}
