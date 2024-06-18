namespace UploadFilesBlobStorage.Configuration
{
    public class BlobStorageConfiguration
    {
        public static string SectionName = "AzureBlobStorage";
        public string ConnectionString { get; set; }
        public string ContainerName { get; set; }
        public string FolderName { get; set; }
    }
}