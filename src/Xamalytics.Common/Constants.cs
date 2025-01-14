namespace Xamalytics.Common
{
    public static class Constants
    {
        public static string[] CompressedFilesExtensions = {"zip"};

        public static string FileIngestionWorkerUserName = "System";

        public const string CorsOrigins = "CorsOrigins";

        public const string SuperAdministratorGroupName = "SuperAdministratorUsers";
        public const string AdministratorGroupName = "AdministratorUsers";
        public const string StandardGroupName = "StandardUsers";

        public const int DefaultBatchWorkerExecutionInterval = 5000;
    }
}
