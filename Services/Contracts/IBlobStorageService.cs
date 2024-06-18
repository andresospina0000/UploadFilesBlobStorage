namespace BlobStorageDemo.Services.Contracts;

public interface IBlobStorageService 
{
    Task<string> UploadFileToBlobAsync(string filePath);
    Task<string> DownloadFileFromBlobAsync(string fileName);
    Task DeleteFileFromBlobAsync(string fileName);
}