using Microsoft.Extensions.Options;
using ProSphere.Domain.Constants.FileConstants;
using ProSphere.ExternalServices.Interfaces.FileStorage;
using ProSphere.Options;
using Supabase;

namespace ProSphere.ExternalServices.Implementaions.FileStorage
{
    public class FileService : IFileService
    {
        private readonly Client _client;
        private readonly string _bucketName;

        public FileService(Client client, IOptions<SupaOptions> options)
        {
            _client = client;
            _bucketName = options.Value.BucketName;
        }

        public async Task<string> UploadAsync(IFormFile file, string? folder = null)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var fileBytes = ms.ToArray();

            var bucket = _client.Storage.From(_bucketName);
            var fileName = folder != null
                ? $"{folder}/{Guid.NewGuid()}_{file.FileName}"
                : $"{Guid.NewGuid()}_{file.FileName}";

            await bucket.Upload(fileBytes, fileName);

            // Remove trailing ? from public URL
            var imageBucketUrl = bucket.GetPublicUrl(fileName)?.TrimEnd('?')!;
            var imagePath = imageBucketUrl.Substring(SupabaseConstants.StartIndexOfCorrectPath);
            return imagePath;
        }


        public async Task<string> EditAsync(string existingFilePath, IFormFile newFile)
        {
            await DeleteAsync(existingFilePath);

            int index = existingFilePath.LastIndexOf("/");

            string folder = existingFilePath.Substring(0, index);

            return await UploadAsync(newFile, folder);
        }

        public async Task DeleteAsync(string filePath)
        {
            var bucket = _client.Storage.From(_bucketName);
            await bucket.Remove(filePath);
        }
    }
}
