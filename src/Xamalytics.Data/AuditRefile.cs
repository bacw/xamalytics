using System;
using System.Collections.Generic;

namespace Xamalytics.Data
{
    public partial class AuditRefile
    {
        public long Id { get; set; }
        public long RefileId { get; set; }
        public string? OldRowData { get; set; }
        public string? NewRowData { get; set; }
        public string DmlType { get; set; } = null!;
        public DateTime DmlTimestamp { get; set; }
        public string DmlCreatedBy { get; set; } = null!;
        public DateTime TransactionTimestamp { get; set; }
    }
}
