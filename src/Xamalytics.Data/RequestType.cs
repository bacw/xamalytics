using System;
using System.Collections.Generic;

namespace Xamalytics.Data
{
    public partial class RequestType
    {
        public RequestType()
        {
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Request> Requests { get; set; }
    }
}
