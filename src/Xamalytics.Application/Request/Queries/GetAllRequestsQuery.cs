using AutoMapper;
using Xamalytics.Common;
using Xamalytics.Dto;
using Xamalytics.Services.Interface;
using Xamalytics.Services.Interface.Common;

namespace Xamalytics.Application.Request.Queries
{
    public class GetAllRequestsQuery : IRequestWrapper<List<RequestDto>>
    {

    }

    public class GetRequestsQueryHandler : IRequestHandlerWrapper<GetAllRequestsQuery, List<RequestDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRequestService _requestService;

        public GetRequestsQueryHandler(IRequestService requestService, IMapper mapper)
        {
            _requestService = requestService;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<RequestDto>>> Handle(GetAllRequestsQuery request, CancellationToken cancellationToken)
        {
            var list = (await _requestService.GetRequests()).ToList();

            return ServiceResult.Success(list);
        }
    }
}
