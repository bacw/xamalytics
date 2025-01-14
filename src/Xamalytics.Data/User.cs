using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamalytics.Data
{
    public partial class User
    {
        public long Id { get; set; }
        public int ClaimStatusId { get; set; }
        public string? Username { get; set; }
        public string? Token { get; set; }
        public DateTime CreatedDate { get; set; }

        public ClaimStatuses? ClaimStatus { get; set; }
    }
}
