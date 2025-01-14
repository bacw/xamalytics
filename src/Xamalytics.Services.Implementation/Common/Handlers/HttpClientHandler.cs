using System.Net.Http.Json;
using Xamalytics.Common;
using Xamalytics.Services.Interface.Common;
using Microsoft.Extensions.Logging;

namespace Xamalytics.Services.Implementation.Common.Handlers
{
    public class HttpClientHandler : IHttpClientHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<HttpClientHandler> _logger;

        public HttpClientHandler(IHttpClientFactory httpClientFactory, ILogger<HttpClientHandler> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<ServiceResult<TResult>> GenericRequest<TRequest, TResult>(string clientApi, string url, CancellationToken cancellationToken,
            Enums.MethodType method = Enums.MethodType.Get,
            TRequest requestRequest = null)
            where TResult : class where TRequest : class
        {
            var httpClient = _httpClientFactory.CreateClient(clientApi);

            var requestName = typeof(TRequest).Name;

            try
            {
                _logger.LogInformation("HttpClient Request: {RequestName} {@Request}", requestName, requestRequest);

                var response = method switch
                {
                    Enums.MethodType.Get => await httpClient.GetAsync(url, cancellationToken),
                    Enums.MethodType.Post => await httpClient.PostAsJsonAsync(url, requestRequest, cancellationToken),
                    _ => null
                };

                if (response != null && response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<TResult>(cancellationToken: cancellationToken);
                    return ServiceResult.Success(data);
                }

                if (response == null)
                    return ServiceResult.Failed<TResult>(ServiceError.ServiceProvider);

                var message = await response.Content.ReadAsStringAsync(cancellationToken);

                var error = new ServiceError(message, (int)response.StatusCode);

                return ServiceResult.Failed<TResult>(error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "HttpClient Request: Unhandled Exception for Request {RequestName} {@Request}", requestName, requestRequest);
                return ServiceResult.Failed<TResult>(ServiceError.CustomMessage(ex.ToString()));
            }
        }
    }
}