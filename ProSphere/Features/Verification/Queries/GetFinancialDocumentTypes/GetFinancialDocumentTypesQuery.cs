using MediatR;

namespace ProSphere.Features.Verification.Queries.GetFinancialDocumentTypes
{
    public record GetFinancialDocumentTypesQuery : IRequest<List<GetFinancialDocumentTypesResponse>>
    {
    }
}
