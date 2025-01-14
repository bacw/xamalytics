using System;
using System.Collections.Generic;

namespace Xamalytics.Data
{
    public partial class StagingRefile
    {
        public long Id { get; set; }
        public long? StagingIngestionId { get; set; }
        public string? ExternalDocumentId { get; set; }
        public string? SourceExternalDocumentId { get; set; }
        public string? SourceMrn { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }

        public virtual StagingIngestion? StagingIngestion { get; set; }
    }
}
