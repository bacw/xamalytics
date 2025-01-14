using System;
using System.Collections.Generic;

namespace Xamalytics.Data
{
    public partial class Ingestion
    {
        public Ingestion()
        {
            Documents = new HashSet<Document>();
            Patients = new HashSet<Patient>();
            Refiles = new HashSet<Refile>();
        }

        public long Id { get; set; }
        public int IngestionTypeId { get; set; }
        public int IngestionStatusId { get; set; }
        public long RequestId { get; set; }
        public string Input { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }

        public virtual IngestionStatus IngestionStatus { get; set; } = null!;
        public virtual IngestionType IngestionType { get; set; } = null!;
        public virtual Request Request { get; set; } = null!;
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
        public virtual ICollection<Refile> Refiles { get; set; }
    }
}
