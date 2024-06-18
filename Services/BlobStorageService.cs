using Azure.Storage.Blobs;
using BlobStorageDemo.Services.Contracts;
using UploadFilesBlobStorage.Configuration;

namespace BlobStorageDemo.Services;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly BlobContainerClient _blobContainerClient;
    private readonly BlobStorageConfiguration _blobStorageConfiguration;

    public BlobStorageService(BlobStorageConfiguration blobStorageConfiguration)
    {
        _blobStorageConfiguration = blobStorageConfiguration;
        _blobServiceClient = new BlobServiceClient(_blobStorageConfiguration.ConnectionString);
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(_blobStorageConfiguration.ContainerName);
    }

    public async Task<string> UploadFileToBlobAsync(string filePath)
    {
        var blobClient = _blobContainerClient.GetBlobClient($"{_blobStorageConfiguration.FolderName}/{Path.GetFileName(filePath)}");
        FileStream fileStream = File.OpenRead(filePath);
        await blobClient.UploadAsync(fileStream);
        fileStream.Close();
        return blobClient.Uri.AbsoluteUri;
    }

    public async Task<string> DownloadFileFromBlobAsync(string fileName)
    {
        var blobClient = _blobContainerClient.GetBlobClient($"{_blobStorageConfiguration.FolderName}/{fileName}");
        var downloadFilePath = Path.Combine(Path.GetTempPath(), fileName);
        await blobClient.DownloadToAsync(downloadFilePath);
        return downloadFilePath;
    }

    public async Task DeleteFileFromBlobAsync(string fileName)
    {
        var blobClient = _blobContainerClient.GetBlobClient($"{_blobStorageConfiguration.FolderName}/{fileName}");
        await blobClient.DeleteIfExistsAsync();
    }
}