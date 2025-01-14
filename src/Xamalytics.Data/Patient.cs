using System;
using System.Collections.Generic;

namespace Xamalytics.Data
{
    public partial class Patient
    {
        public Patient()
        {
            Documents = new HashSet<Document>();
        }

        public long Id { get; set; }
        public long LastIngestionId { get; set; }
        public string? Nhsnumber { get; set; }
        public string? Mrn { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Dob { get; set; }
        public string? Address { get; set; }
        public string? HomePhoneNumber { get; set; }
        public string? WorkPhoneNumber { get; set; }
        public string? MobilePhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; } = string.Empty;
        public DateTime LastModifiedDate { get; set; }

        public virtual Ingestion LastIngestion { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}
