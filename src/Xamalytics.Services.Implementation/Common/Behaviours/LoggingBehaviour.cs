using MediatR.Pipeline;
using Xamalytics.Services.Interface.Common;
using Serilog;

namespace Xamalytics.Services.Implementation.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IIdentityService _idrequestService;

        public LoggingBehaviour(Serilog.ILogger logger, ICurrentUserService currentUserService, IIdentityService idrequestService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _idrequestService = idrequestService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId ?? string.Empty;
            string userName = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                userName = await _idrequestService.GetUserNameAsync(userId);
            }

            _logger.Error("Request: {Name} {@UserId} {@UserName} {@Request}",
                requestName, userId, userName, request);
        }
    }
}
