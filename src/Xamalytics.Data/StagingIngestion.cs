using System;
using System.Collections.Generic;

namespace Xamalytics.Data
{
    public partial class StagingIngestion
    {
        public StagingIngestion()
        {
            StagingMetadata = new HashSet<StagingMetadatum>();
            StagingPatients = new HashSet<StagingPatient>();
            StagingRefiles = new HashSet<StagingRefile>();
        }

        public long Id { get; set; }
        public int? IngestionTypeId { get; set; }
        public long RequestId { get; set; }
        public string? Input { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }

        public virtual Request Request { get; set; } = null!;
        public virtual ICollection<StagingMetadatum> StagingMetadata { get; set; }
        public virtual ICollection<StagingPatient> StagingPatients { get; set; }
        public virtual ICollection<StagingRefile> StagingRefiles { get; set; }
    }
}
