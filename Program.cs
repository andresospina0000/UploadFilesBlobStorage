using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using UploadFilesBlobStorage.Configuration;
using Microsoft.Extensions.Configuration;
using BlobStorageDemo.Services.Contracts;
using BlobStorageDemo.Services;
using UploadFilesBlobStorage.Repositories;
using UploadFilesBlobStorage.Repositories.Contracts;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddOptions<DBConfiguration>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection(DBConfiguration.SectionName).Bind(settings);
            });

        services.AddOptions<BlobStorageConfiguration>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection(BlobStorageConfiguration.SectionName).Bind(settings);
            });

        services.AddSingleton<IBlobStorageService, BlobStorageService>();
        services.AddSingleton<IUploadFilesService, UploadFilesService>();
        services.AddSingleton<IRepository, Repository>();

        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();
