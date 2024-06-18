namespace UploadFilesBlobStorage.Configuration
{
    public class DBConfiguration
    {
        public static string SectionName = "DBSettings";
        public string ConnectionString { get; set; }
        public int TimeOut { get; set; }
    }
}