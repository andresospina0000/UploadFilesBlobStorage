using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlobStorageDemo.Services.Contracts;

namespace Company.Function
{
    public class UploadCsvFile
    {
        private readonly ILogger<UploadCsvFile> _logger;
        private readonly IUploadFilesService _uploadFilesService;

        public UploadCsvFile(ILogger<UploadCsvFile> logger, IUploadFilesService uploadFilesService)
        {
            _logger = logger;
            _uploadFilesService = uploadFilesService;
        }

        [Function("UploadCsv")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            await _uploadFilesService.UploadFileToBlobAsync();
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}