using System;
using System.Collections.Generic;

namespace Xamalytics.Data
{
    public partial class Refile
    {
        public long Id { get; set; }
        public long LastIngestionId { get; set; }
        public long DocumentId { get; set; }
        public string SourceExternalDocumentId { get; set; } = string.Empty;
        public string SourcePatientMrn { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }

        public virtual Document Document { get; set; } = null!;
        public virtual Ingestion LastIngestion { get; set; } = null!;
    }
}
