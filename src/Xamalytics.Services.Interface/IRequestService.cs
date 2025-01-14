using Xamalytics.Dto;
using Xamalytics.Common;


namespace Xamalytics.Services.Interface
{
    public interface IRequestService
    {
        Task<IEnumerable<RequestDto>> GetRequests();
        Task<IList<RequestDto>> GetRequestsByDate(DateTime requestDate, CancellationToken cancellationToken);

       Task<IList<RequestDto>> GetRequestsByBatch(long batchId, CancellationToken cancellationToken);

        Task<RequestDto> GetRequest(long id, CancellationToken cancellationToken);

        Task<bool> UpdateRequest(long id, RequestDto? requestDto, CancellationToken cancellationToken);

        Task<RequestDto?> AddRequest(RequestDto? requestDto, CancellationToken cancellationToken);

        Task<bool> DeleteRequest(long id, CancellationToken cancellationToken);
    } 
}