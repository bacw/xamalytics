using Xamalytics.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamalytics.Dto
{
    public class AuditUserDto
    {
        public long Id { get; set; }
        public string? UserName { get; set; }
        public int ActivityTypeId { get; set; }
        public DateTime ActivityDate { get; set; }
        public string? ActivityDetails { get; set; }
        public string? ActivityTypeDescription { get; set; }
       
    }
}
