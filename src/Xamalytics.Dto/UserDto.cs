using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamalytics.Dto
{
    public class UserDto
    {
        public long Id { get; set; }
        public int ClaimStatusId { get; set; }
        public string? Username { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Token { get; set; }
        public bool? IsAuthenticated { get; set; }
    }
}
