using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UploadFilesBlobStorage.Configuration;
using UploadFilesBlobStorage.Repositories.Contracts;

namespace UploadFilesBlobStorage.Repositories
{
    public class Repository : IRepository
    {
        private readonly DBConfiguration _dbConfiguration;
        private readonly ILogger<Repository> _logger;

        public Repository(IOptions<DBConfiguration> dbConfiguration, ILogger<Repository> logger)
        {
            _dbConfiguration = dbConfiguration.Value;
            _logger = logger;
        }

        public DataTable GetData()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConfiguration.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Data]", connection))
                    {
                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                        {
                            dataAdapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetData");
            }
            return dataTable;
        }
    }
}