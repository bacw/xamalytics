using System;
using System.Collections.Generic;

namespace Xamalytics.Data
{
    public partial class StagingRequestStatus
    {
        public StagingRequestStatus()
        {
            StagingRequests = new HashSet<StagingRequest>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<StagingRequest> StagingRequests { get; set; }
    }
}
