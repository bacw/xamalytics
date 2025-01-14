using Xamalytics.Common;

namespace Xamalytics.Services.Interface.Common
{
    public interface IHttpClientHandler
    {
        Task<ServiceResult<TResult>> GenericRequest<TRequest, TResult>(string clientApi, string url,
            CancellationToken cancellationToken,
            Enums.MethodType method = Enums.MethodType.Get,
            TRequest requestRequest = null)
            where TResult : class where TRequest : class;
    }
}