using AutoMapper;
using Xamalytics.Common;
using Xamalytics.Dto;
using Xamalytics.Services.Interface;
using Xamalytics.Services.Interface.Common;

namespace Xamalytics.Application.Request.Commands
{
    public class DeleteRequestCommand : IRequestWrapper<RequestDto>
    {
        public int Id { get; set; }
    }

    public class DeleteRequestCommandHandler : IRequestHandlerWrapper<DeleteRequestCommand, RequestDto>
    {
        private readonly IMapper _mapper;
        private readonly IRequestService _requestService;

        public DeleteRequestCommandHandler(IRequestService requestService, IMapper mapper)
        {
            _requestService = requestService;
            _mapper = mapper;
        }

        public async Task<ServiceResult<RequestDto>> Handle(DeleteRequestCommand request, CancellationToken cancellationToken)
        {
            var isSuccessful = await _requestService.DeleteRequest(request.Id, cancellationToken);

            var requestDto = _mapper.Map<RequestDto>(request);

            return isSuccessful ? ServiceResult.Success(requestDto) : ServiceResult.Failed<RequestDto>(ServiceError.DefaultError);

        }
    }
}
