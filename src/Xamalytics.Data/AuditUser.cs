using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamalytics.Data
{
    public partial class AuditUser
    {
        public long Id { get; set; }
        public string? UserName { get; set; }
        public int ActivityTypeId { get; set; }
        public DateTime ActivityDate { get; set; }
        public string? ActivityDetails { get; set; }
        public virtual ActivityType ActivityType { get; set; }
    }
}
