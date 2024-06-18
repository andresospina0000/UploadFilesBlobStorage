namespace BlobStorageDemo.Services.Contracts;

public interface IUploadFilesService
{
    Task<string> UploadFileToBlobAsync();
}