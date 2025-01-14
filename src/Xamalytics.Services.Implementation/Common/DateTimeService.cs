using Xamalytics.Services.Interface.Common;

namespace Xamalytics.Services.Implementation.Common
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
