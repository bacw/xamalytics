using AutoMapper;
using AutoMapper.QueryableExtensions;
using Xamalytics.Common.Exceptions;
using Xamalytics.Data;
using Xamalytics.Data.Context;
using Xamalytics.Dto;
using Xamalytics.Services.Interface;
using Xamalytics.Services.Interface.Common;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;

namespace Xamalytics.Services.Implementation
{
    public class RequestService : IRequestService
    {
        private readonly XamalyticsContext _xamalyticsContext;
        private readonly IDateTimeService _dateTimeService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public RequestService(XamalyticsContext xamalyticsContext, IDateTimeService dateTimeService, IMapper mapper, ILogger logger)
        {
            _xamalyticsContext = xamalyticsContext;
            _dateTimeService = dateTimeService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<RequestDto>> GetRequests()
        {
            var requests = await _xamalyticsContext.Requests
                                                .Include(j => j.RequestType)
                                                .Include(j => j.RequestStatus)
                                                .ToListAsync();

            var query = from request in requests
                        select new RequestDto
                        {
                            Id = request.Id,
                            BatchId = request.BatchId,
                            RequestStatusDescription = request.RequestStatus.Name,
                            RequestTypeDescription = request.RequestType.Name,
                            Reference = request.Reference,
                            ProcessStartDate = request.ProcessStartDate,
                            ProcessEndDate = request.ProcessEndDate
                        };

            return query.ToList();
        }

        public async Task<IList<RequestDto>> GetRequestsByDate(DateTime requestDate, CancellationToken cancellationToken)
        {
            return await _xamalyticsContext.Requests
                                                .Where(r => r.CreatedDate.Date >= requestDate.Date)
                                                .ProjectTo<RequestDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }

        public async Task<IList<RequestDto>> GetRequestsByBatch(long id, CancellationToken cancellationToken)
        {
            var requests = await _xamalyticsContext.Requests
                                               .Include(j => j.RequestType)
                                               .Include(j => j.RequestStatus)
                                               .ToListAsync(cancellationToken: cancellationToken);


            var query = from request in requests
                        select new RequestDto
                        {
                            Id = request.Id,
                            BatchId = request.BatchId,
                            RequestStatusDescription = request.RequestStatus.Name,
                            RequestTypeDescription = request.RequestType.Name,
                            Reference = request.Reference,
                            ProcessStartDate = request.ProcessStartDate,
                            ProcessEndDate = request.ProcessEndDate
                        };

            return query.ToList();
        }

        public async Task<RequestDto> GetRequest(long id, CancellationToken cancellationToken)
        {
            var request = await _xamalyticsContext.Requests
                .Where(x => x.Id == id)
                .ProjectTo<RequestDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return request!;
        }

        public async Task<bool> UpdateRequest(long id, RequestDto? requestDtoDto, CancellationToken cancellationToken)
        {
            var request = await _xamalyticsContext.Requests.FindAsync(id);

            if (request == null)
            {
                throw new NotFoundException(nameof(Request), id);
            }

            _mapper.Map(requestDtoDto, request);

            request.LastModifiedDate = _dateTimeService.Now;

            _xamalyticsContext.Entry(request).State = EntityState.Modified;

            try
            {
                await _xamalyticsContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                _logger.Error(exception.Message, $"Could not update requestDto. Details: {JsonConvert.SerializeObject(request)}");
                if (!RequestExists(id))
                {
                    return false;
                }

                throw;
            }
            catch (Exception exception)
            {
                _logger.Error(exception.Message, $"Could not update requestDto. Details: {JsonConvert.SerializeObject(request)}");
                throw;
            }
        }

        public async Task<RequestDto?> AddRequest(RequestDto? requestDto, CancellationToken cancellationToken)
        {
            if(requestDto == null) throw new ArgumentNullException(nameof(requestDto));

            requestDto.CreatedDate = _dateTimeService.Now;
            requestDto.LastModifiedDate = _dateTimeService.Now;

            var request = _mapper.Map<Request>(requestDto);
            await _xamalyticsContext.Requests.AddAsync(request, cancellationToken);
            try
            {
                await _xamalyticsContext.SaveChangesAsync(cancellationToken);
                requestDto.Id = request.Id;
            }
            catch (DbUpdateException exception)
            {
                _logger.Error(exception.Message, $"Could not add requestDto. Details: {JsonConvert.SerializeObject(request)}");
                if (RequestExists(request.Id))
                {
                    return null!;
                }
                throw;
            }
            catch (Exception exception)
            {
                _logger.Error(exception.Message, $"Could not add requestDto. Details: {JsonConvert.SerializeObject(request)}");
                throw;
            }
            return requestDto;
        }

        public async Task<bool> DeleteRequest(long id, CancellationToken cancellationToken)
        {
            var request = await _xamalyticsContext.Requests
                .Where(l => l.Id == id)
                .SingleOrDefaultAsync(cancellationToken);

            if (request == null)
            {
                throw new NotFoundException(nameof(Request), id);
            }

            try
            {
                _xamalyticsContext.Requests.Remove(request);
                await _xamalyticsContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException exception)
            {
                _logger.Error(exception.Message, $"Could not delete requestDto. Details: {JsonConvert.SerializeObject(request)}");
                return false;
            }

            return true;
        }

        private bool RequestExists(long id)
        {
            return _xamalyticsContext.Requests.Any(e => e.Id == id);
        }

      
    }
}