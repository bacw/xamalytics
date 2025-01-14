using System;
using System.Collections.Generic;

namespace Xamalytics.Data
{
    public partial class StagingRequest
    {
        public long Id { get; set; }
        public string? BatchFileName { get; set; }
        public int StagingRequestStatusId { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }

        public virtual StagingRequestStatus StagingRequestStatus { get; set; } = null!;
    }
}
