using System;
using System.Collections.Generic;

namespace Xamalytics.Data
{
    public partial class BatchSource
    {
        public BatchSource()
        {
            BatchSourceConfigs = new HashSet<BatchSourceConfig>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<BatchSourceConfig> BatchSourceConfigs { get; set; }
    }
}
