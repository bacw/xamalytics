using AutoMapper;
using Xamalytics.Common;
using Xamalytics.Dto;
using Xamalytics.Services.Interface;
using Xamalytics.Services.Interface.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Xamalytics.Application.Request.Commands
{
    public class CreateRequestCommand : IRequestWrapper<RequestDto>
    {
        public IFormFile Image { get; set; }
    }

    public class CreateRequestCommandHandler : IRequestHandlerWrapper<CreateRequestCommand, RequestDto>
    {
        private readonly IMapper _mapper;
        private readonly IRequestService _requestService;
        private readonly IDateTimeService _dateTimeService;
        private readonly AppSetting _appSetting;
        private readonly Serilog.ILogger _logger;

        public CreateRequestCommandHandler(IRequestService requestService,
                                           IDateTimeService dateTimeService,
                                           IMapper mapper,
                                           Serilog.ILogger logger,
                                           IOptions<AppSetting> options)
        {
            _mapper = mapper;
            _requestService = requestService;
            _dateTimeService = dateTimeService;
            _logger = logger;
            _appSetting = options.Value;
        }
        public async Task<ServiceResult<RequestDto>> Handle(CreateRequestCommand createCommandRequest, CancellationToken cancellationToken)
        {
         
            if (string.IsNullOrEmpty(_appSetting.DocumentRoot))
                return ServiceResult.Failed<RequestDto>(ServiceError.InvalidDocumentStoreRoot);

            var requestDto = new RequestDto
            {
                RequestTypeId = (int)Enums.RequestType.Api,
                RequestStatusId = (int)Enums.RequestStatus.Created,
                ProcessStartDate = _dateTimeService.Now,
                CreatedBy = Constants.FileIngestionWorkerUserName,
                CreatedDate = _dateTimeService.Now,
                LastModifiedBy = Constants.FileIngestionWorkerUserName,
                LastModifiedDate = _dateTimeService.Now
            };

            var requestResult = await _requestService.AddRequest(requestDto, cancellationToken);
            if (requestResult == null) return ServiceResult.Failed<RequestDto>(ServiceError.DefaultError);

            requestDto.RequestStatusId = (int)Enums.RequestStatus.Completed;
            requestDto.ProcessEndDate = _dateTimeService.Now;
            requestDto.LastModifiedBy = Constants.FileIngestionWorkerUserName;
            requestDto.LastModifiedDate = _dateTimeService.Now;

            await _requestService.UpdateRequest(requestDto.Id, requestDto, cancellationToken);

            return ServiceResult.Success(_mapper.Map<RequestDto>(requestDto));
        }
    }
}
