using Xamalytics.Data;

namespace Xamalytics.Dto
{
    public class RequestDto
    {
        public long Id { get; set; }
        public long? BatchId { get; set; }
        public string? Reference { get; set; }
        public int RequestTypeId { get; set; }
        public int RequestStatusId { get; set; }
        public DateTime ProcessStartDate { get; set; }
        public DateTime? ProcessEndDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }
        public string RequestStatusDescription { get; set; } = string.Empty;
        public string RequestTypeDescription { get; set; } = string.Empty;
        public  RequestStatus RequestStatus { get; set; }
        public  RequestType RequestType { get; set; }
    }
}