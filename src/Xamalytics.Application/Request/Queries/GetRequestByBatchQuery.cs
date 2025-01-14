using AutoMapper;
using MediatR;
using Xamalytics.Common;
using Xamalytics.Dto;
using Xamalytics.Services.Interface;
using Xamalytics.Services.Interface.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Xamalytics.Application.Request.Queries
{
    public class GetRequestByBatchQuery : IRequestWrapper<List<RequestDto>>
    {
        public long BatchId { get; set; }
    }

    public class GetRequestByBatchQueryHandler : IRequestHandlerWrapper<GetRequestByBatchQuery, List<RequestDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRequestService _requestService;

        public GetRequestByBatchQueryHandler(IRequestService requestService, IMapper mapper)
        {
            _mapper = mapper;
            _requestService = requestService;
        }



        public async Task<ServiceResult<List<RequestDto>>> Handle (GetRequestByBatchQuery getRequestByBatch, CancellationToken cancellationToken)
        {
            var result = (await _requestService.GetRequestsByBatch(getRequestByBatch.BatchId, cancellationToken)).ToList();

            return result != null ? ServiceResult.Success(result) : ServiceResult.Failed<List<RequestDto>>(ServiceError.NotFound);
        }

    } 
}
