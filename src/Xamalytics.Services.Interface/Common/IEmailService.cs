using Xamalytics.Common;

namespace Xamalytics.Services.Interface.Common
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}
