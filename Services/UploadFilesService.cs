using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using BlobStorageDemo.Services.Contracts;
using Microsoft.Extensions.Logging;
using UploadFilesBlobStorage.Repositories.Contracts;

namespace BlobStorageDemo.Services;

public class UploadFilesService : IUploadFilesService
{
    private readonly IRepository repository;
    private readonly IBlobStorageService blobStorageService;
    private readonly ILogger<UploadFilesService> logger;
    public UploadFilesService(ILogger<UploadFilesService> logger, IRepository repository, IBlobStorageService blobStorageService)
    {
        this.logger = logger;
        this.repository = repository;
        this.blobStorageService = blobStorageService;
    }

    public Task<string> UploadFileToBlobAsync()
    {
        DataTable dataTable = repository.GetData();
        string filePath = "./Data.csv";
        GenerateCSVFile(dataTable, filePath);
        return blobStorageService.UploadFileToBlobAsync(filePath);
    }

    public void GenerateCSVFile(DataTable dataTable, string filePath)
    {
        StringBuilder sb = new StringBuilder();

        IEnumerable<string> columnNames = dataTable.Columns
                                                   .Cast<DataColumn>()
                                                   .Select(column => column.ColumnName);

        sb.AppendLine(string.Join(",", columnNames));

        foreach (DataRow row in dataTable.Rows)
        {
            IEnumerable<string> data = row.ItemArray.Select(field => field.ToString());
            sb.AppendLine(string.Join(",", data));
        }

        File.WriteAllText(filePath, sb.ToString());
    }
}
