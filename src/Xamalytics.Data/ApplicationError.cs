using System;
using System.Collections.Generic;

namespace Xamalytics.Data
{
    public partial class ApplicationError
    {
        public long Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string? MachineName { get; set; }
        public string? Environment { get; set; }
        public string? Thread { get; set; }
        public string? LogLevel { get; set; }
        public string? Logger { get; set; }
        public string? Message { get; set; }
        public string? Exception { get; set; }
    }
}
