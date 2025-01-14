using System;
using System.Collections.Generic;

namespace Xamalytics.Data
{
    public partial class IngestionType
    {
        public IngestionType()
        {
            Ingestions = new HashSet<Ingestion>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Ingestion> Ingestions { get; set; }
    }
}
