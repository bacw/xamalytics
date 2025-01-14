using AutoMapper;
using Xamalytics.Common;
using Xamalytics.Dto;
using Xamalytics.Services.Interface;
using Xamalytics.Services.Interface.Common;

namespace Xamalytics.Application.Request.Commands
{
    public class UpdateRequestCommand : IRequestWrapper<RequestDto>
    {
        public long Id { get; set; }    
        public byte[] File { get; set; }
    }

    public class UpdateRequestCommandHandler : IRequestHandlerWrapper<UpdateRequestCommand, RequestDto>
    {
        private readonly IMapper _mapper;
        private readonly IRequestService _requestService;

        public UpdateRequestCommandHandler(IRequestService requestService, IMapper mapper)
        {
            _requestService = requestService;
            _mapper = mapper;
        }

        public async Task<ServiceResult<RequestDto>> Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
        {
            var requestDto = _mapper.Map<RequestDto>(request);
            await _requestService.UpdateRequest(request.Id, requestDto, cancellationToken);

            return ServiceResult.Success(requestDto);
        }
    }
}
