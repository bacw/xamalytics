using AutoMapper;
using Xamalytics.Common;
using Xamalytics.Dto;
using Xamalytics.Services.Interface;
using Xamalytics.Services.Interface.Common;

namespace Xamalytics.Application.Request.Queries
{
    public class GetRequestByIdQuery : IRequestWrapper<RequestDto>
    {
        public int RequestId { get; set; }
    }

    public class GetRequestByIdQueryHandler : IRequestHandlerWrapper<GetRequestByIdQuery, RequestDto>
    {
        private readonly IMapper _mapper;
        private readonly IRequestService _requestService;

        public GetRequestByIdQueryHandler(IRequestService requestService, IMapper mapper)
        {
            _mapper = mapper;
            _requestService = requestService;
        }

        public async Task<ServiceResult<RequestDto>> Handle(GetRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _requestService.GetRequest(request.RequestId, cancellationToken);

            return entity != null ? ServiceResult.Success(entity) : ServiceResult.Failed<RequestDto>(ServiceError.NotFound);
        }
    }
}
