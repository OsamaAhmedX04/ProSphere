using MediatR;
using Microsoft.Extensions.Caching.Memory;
using ProSphere.Domain.Constants.CacheConstants;
using ProSphere.RepositoryManager.Interfaces;

namespace ProSphere.Features.Verification.Queries.GetProfessionalDocumentTypes
{
    // still work on it
    public class GetProfessionalDocumentTypesQueryHandler
        : IRequestHandler<GetProfessionalDocumentTypesQuery, List<GetProfessionalDocumentTypesResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public GetProfessionalDocumentTypesQueryHandler(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<List<GetProfessionalDocumentTypesResponse>>
            Handle(GetProfessionalDocumentTypesQuery query, CancellationToken cancellationToken)
        {
            if (_cache.TryGetValue(CacheKey.ProfessionalDocumentTypes, out List<GetProfessionalDocumentTypesResponse> result))
                return result;

            result = await _unitOfWork.ProfessionalDocumentTypes.GetAllAsyncEnhanced(
                selector: x => new GetProfessionalDocumentTypesResponse
                {
                    DocumentTypeId = x.Id,
                    Name = x.Name
                });

            var cacheEntryOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
            };

            _cache.Set(CacheKey.ProfessionalDocumentTypes, result, cacheEntryOptions);

            return result;
        }
    }
}
