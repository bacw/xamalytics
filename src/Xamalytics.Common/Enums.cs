namespace Xamalytics.Common
{
    public static class Enums
    {
        public enum ActivityType
        {
            ViewPatientsDetails = 1,
            ViewDocumentDetails = 2,
            LogIn = 3,
            LogOut = 4,
        }
        public enum BatchStatus
        {
            None = -1,
            Created = 0,
            Duplicate = 1,
            Completed = 2,
            Failed = 3,
            Inprogress = 4,
            PartialSucceeded = 5,
        }

        public enum ClaimsStatuses
        {
            Administrator = 1,
            Standard = 2,
            SuperAdminstrator = 3,
        }


        public enum RequestStatus
        {
            None = -1,
            Created = 0,
            Completed = 1,
            PartiallyCompleted = 2,
            Failed = 3,
            Duplicate = 4,
        }

        public enum RequestType
        {
            None = -1,
            Batch = 0,
            Api = 1,
        }

        public enum IngestionType
        {
            None = -1,
            Manifest = 0,
            Document = 1,
        }

        public enum FileIngestionStatus
        {
            None = -1,
            IngestionStarted = 0,
            IngestionCompleted = 1,
            IngestionFailed = 2
        }


        public enum IngestionStatus
        {
            None = -1,
            IngestionStarted = 0,
            IngestionCompleted = 1,
            IngestionFailed = 2,
        }

        public enum FileType
        {
            None = -1,
            Document = 0,
            Manifest = 1,
        }
        
        public enum MethodType
        {
            Post = 0,
            Get = 1
        }

        public enum StagingRequestStatus
        {
            None = -1,
            Created = 0,
            Completed = 1,
            Failed = 2,
            Duplicate = 3,
        }
    }
}
