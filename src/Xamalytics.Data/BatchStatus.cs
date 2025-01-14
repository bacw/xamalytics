using System;
using System.Collections.Generic;

namespace Xamalytics.Data
{
    public partial class BatchStatus
    {
        public BatchStatus()
        {
            Batches = new HashSet<Batch>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Batch> Batches { get; set; }
    }
}
