using MediatR;
using Xamalytics.Common;
using Xamalytics.Dto;

namespace Xamalytics.Services.Interface.Common
{
    public interface IRequestWrapper<T> : IRequest<ServiceResult<T>>
    {

    }

    public interface IRequestHandlerWrapper<TIn, TOut> : IRequestHandler<TIn, ServiceResult<TOut>> where TIn : IRequestWrapper<TOut>
    {
    }
}
