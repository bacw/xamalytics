using AutoMapper;
using Xamalytics.Common;
using Xamalytics.Dto;
using Xamalytics.Services.Interface.Common;
using Xamalytics.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamalytics.Application.Request.Queries
{
    public class GetRequestByStatusQuery : IRequestWrapper<RequestDto>
    {
        public int RequestStatus { get; set; }
    }

    public class GetRequestByStatusQueryHandler : IRequestHandlerWrapper<GetRequestByStatusQuery, RequestDto>
    {
        private readonly IMapper _mapper;
        private readonly IRequestService _requestService;

        public GetRequestByStatusQueryHandler(IRequestService requestService, IMapper mapper)
        {
            _mapper = mapper;
            _requestService = requestService;
        }

        public async Task<ServiceResult<RequestDto>> Handle(GetRequestByStatusQuery request, CancellationToken cancellationToken)
        {
            var entity = await _requestService.GetRequest(request.RequestStatus, cancellationToken);

            return entity != null ? ServiceResult.Success(entity) : ServiceResult.Failed<RequestDto>(ServiceError.NotFound);
        }
    }
}
