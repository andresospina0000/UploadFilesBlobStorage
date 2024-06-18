using System.Data;

namespace UploadFilesBlobStorage.Repositories.Contracts
{
    public interface IRepository
    {
        DataTable GetData();
    }
}