using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;

namespace ProSphere.Jobs.Documents.DeleteDocumentVerification
{
    public interface IDeleteDocumentVerificationJob
    {
        Task DeleteIdentityVerificationDocuments(string userId, Status status);
        Task DeleteFinancialVerificationDocuments(string userId, Status status);
        Task DeleteProfessionalVerificationDocuments(string userId, Status status);
    }
}
