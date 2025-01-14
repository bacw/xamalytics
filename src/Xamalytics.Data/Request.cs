using System;
using System.Collections.Generic;

namespace Xamalytics.Data
{
    public partial class Request
    {
        public Request()
        {
            Ingestions = new HashSet<Ingestion>();
            StagingIngestions = new HashSet<StagingIngestion>();
        }

        public long Id { get; set; }
        public long? BatchId { get; set; }
        public string? Reference { get; set; } 
        public int RequestTypeId { get; set; }
        public int RequestStatusId { get; set; }
        public DateTime ProcessStartDate { get; set; }
        public DateTime? ProcessEndDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }
        public virtual Batch Batch { get; set; }
        public virtual RequestStatus RequestStatus { get; set; }
        public virtual RequestType RequestType { get; set; }
        public virtual ICollection<Ingestion> Ingestions { get; set; }
        public virtual ICollection<StagingIngestion> StagingIngestions { get; set; }
    }
}