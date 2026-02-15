using MediatR;
using Microsoft.Extensions.Caching.Memory;
using ProSphere.Domain.Constants.CacheConstants;
using ProSphere.RepositoryManager.Interfaces;

namespace ProSphere.Features.Verification.Queries.GetFinancialDocumentTypes
{
    // still work on it
    public class GetFinancialDocumentTypesQueryHandler : IRequestHandler<GetFinancialDocumentTypesQuery, List<GetFinancialDocumentTypesResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public GetFinancialDocumentTypesQueryHandler(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<List<GetFinancialDocumentTypesResponse>>
            Handle(GetFinancialDocumentTypesQuery query, CancellationToken cancellationToken)
        {
            if(_cache.TryGetValue(CacheKey.FinancailDocumentTypes, out List<GetFinancialDocumentTypesResponse> result))
                return result;

            result = await _unitOfWork.FinancialDocumentTypes.GetAllAsyncEnhanced(
                selector: x => new GetFinancialDocumentTypesResponse
                {
                    DocumentTypeId = x.Id,
                    Name = x.Name
                });

            var cacheEntryOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24),
            };

            _cache.Set(CacheKey.ProfessionalDocumentTypes, result, cacheEntryOptions);

            return result;
        }
    }
}
