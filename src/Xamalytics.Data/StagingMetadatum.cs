using System;
using System.Collections.Generic;

namespace Xamalytics.Data
{
    public partial class StagingMetadatum
    {
        public long Id { get; set; }
        public long? StagingIngestionId { get; set; }
        public string? ExternalDocumentId { get; set; }
        public string? DocumentType { get; set; }
        public string? Mrn { get; set; }
        public string? Title { get; set; }
        public string? FileName { get; set; }
        public string? RawCopyFilePath { get; set; }
        public DateTime? DocumentDate { get; set; }
        public int? PageCount { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }

        public virtual StagingIngestion? StagingIngestion { get; set; }
    }
}
