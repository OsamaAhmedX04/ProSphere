using ProSphere.Domain.Constants;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.ExternalServices.Interfaces.FileStorage;
using ProSphere.RepositoryManager.Interfaces;

namespace ProSphere.Jobs.Documents.DeleteDocumentVerification
{
    public class DeleteDocumentVerificationJob : IDeleteDocumentVerificationJob
    {
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDocumentVerificationJob(IFileService fileService, IUnitOfWork unitOfWork)
        {
            _fileService = fileService;
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteFinancialVerificationDocuments(string userId, Status status)
        {
            var documents = await _unitOfWork.FinancialVerifications.GetAllAsyncEnhanced
                (
                filter: v => v.InvestorId == userId && v.status == status,
                selector: v => v.DocumentURL
                );
            await _unitOfWork.FinancialVerifications.BulkDeleteAsync(v => v.InvestorId == userId && v.status == status);

            foreach (var document in documents )
                await _fileService.DeleteAsync(SupabaseConstants.PrefixSupaURL + document);
        }

        public async Task DeleteIdentityVerificationDocuments(string userId, Status status)
        {
            var documents = await _unitOfWork.IdentityVerifications.GetAllAsyncEnhanced
                (
                filter: v => v.UserId == userId && v.status == status,
                selector: v => new
                {
                    FrontIdImageURL = v.IdFrontImageURL,
                    BackIdImageURL = v.IdBackImageURL,
                    SelfieWithIdImageURL = v.SelfieWithIdURL
                }
                );
            await _unitOfWork.IdentityVerifications.BulkDeleteAsync(v => v.UserId == userId && v.status == status);

            foreach (var document in documents)
            {
                await _fileService.DeleteAsync(SupabaseConstants.PrefixSupaURL + document.FrontIdImageURL);
                await _fileService.DeleteAsync(SupabaseConstants.PrefixSupaURL + document.BackIdImageURL);
                await _fileService.DeleteAsync(SupabaseConstants.PrefixSupaURL + document.SelfieWithIdImageURL);
            }
        }

        public async Task DeleteProfessionalVerificationDocuments(string userId, Status status)
        {
            var documents = await _unitOfWork.ProfessionalVerifications.GetAllAsyncEnhanced
                (
                filter: v => v.InvestorId == userId && v.status == status,
                selector: v => v.DocumentURL
                );

            await _unitOfWork.ProfessionalVerifications.BulkDeleteAsync(v => v.InvestorId == userId && v.status == status);

            foreach (var document in documents)
                await _fileService.DeleteAsync(SupabaseConstants.PrefixSupaURL + document);
        }
    }
}
