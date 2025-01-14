using System;
using System.Collections.Generic;

namespace Xamalytics.Data
{
    public partial class Document
    {
        public Document()
        {
            Refiles = new HashSet<Refile>();
        }

        public long Id { get; set; }
        public string ExternalDocumentId { get; set; } = null!;
        public string DocumentType { get; set; } = null!;
        public long LastIngestionId { get; set; }
        public long PatientId { get; set; }
        public string? Title { get; set; }
        public string? FileName { get; set; }
        public string? RawCopyFilePath { get; set; }
        public string? OcrcopyFilePath { get; set; }
        public DateTime? DocumentDate { get; set; }
        public int? PageCount { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }

        public virtual Ingestion LastIngestion { get; set; } = null!;
        public virtual Patient Patient { get; set; } = null!;
        public virtual ICollection<Refile> Refiles { get; set; }
    }
}
