using AutoMapper;
using Xamalytics.Common;
using Xamalytics.Dto;
using Xamalytics.Services.Interface;
using Xamalytics.Services.Interface.Common;

namespace Xamalytics.Application.Request.Queries
{
    public class SearchRequestsQuery : IRequestWrapper<List<RequestDto>>
    {
        public DateTime StartDate { get; set; }
    }

    public class SearchRequestsQueryHandler : IRequestHandlerWrapper<SearchRequestsQuery, List<RequestDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRequestService _requestService;

        public SearchRequestsQueryHandler(IRequestService requestService, IMapper mapper)
        {
            _requestService = requestService;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<RequestDto>>> Handle(SearchRequestsQuery request, CancellationToken cancellationToken)
        {
            var list = (await _requestService.GetRequestsByDate(request.StartDate, cancellationToken)).ToList();

            return ServiceResult.Success(list);
        }
    }
}
