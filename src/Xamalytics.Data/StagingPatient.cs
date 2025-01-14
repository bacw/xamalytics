using System;
using System.Collections.Generic;

namespace Xamalytics.Data
{
    public partial class StagingPatient
    {
        public long Id { get; set; }
        public long StagingIngestionId { get; set; }
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
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }

        public virtual StagingIngestion StagingIngestion { get; set; } = null!;
    }
}
