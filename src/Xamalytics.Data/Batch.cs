using System;
using System.Collections.Generic;

namespace Xamalytics.Data
{
    public partial class Batch
    {
        public long Id { get; set; }
        public int BatchStatusId { get; set; }
        public string? BatchName { get; set; }
        public DateTime ProcessStartDate { get; set; }
        public DateTime? ProcessEndDate { get; set; }
        public int? TotalNumberOfFilesInBatch { get; set; }
        public int? CurrentNumberOfFilesProcessedInBatch { get; set; }
        public string? Output { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }       
        public virtual BatchStatus BatchStatus { get; set; } = null!;
        public virtual Request Request { get; set; } = null!;
    }
}
