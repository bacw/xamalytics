using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamalytics.Data
{
    public partial class ActivityType
    {
        public ActivityType()
        {
            AuditUsers = new HashSet<AuditUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<AuditUser> AuditUsers { get; set; }
    }
}
