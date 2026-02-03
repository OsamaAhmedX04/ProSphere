namespace ProSphere.ExternalServices.Interfaces.FileStorage
{
    public interface IFileService
    {
        Task<string> UploadAsync(IFormFile file, string? folder = null);
        Task<string> EditAsync(string existingFilePath, IFormFile newFile);
        Task DeleteAsync(string filePath);
    }
}
