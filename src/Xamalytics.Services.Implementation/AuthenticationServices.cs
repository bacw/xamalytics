using Xamalytics.Data;
using System.Runtime.InteropServices;

namespace Xamalytics.Services.Implementation
{
    public class AuthenticationServices
    {
        #region DllImoprtLogOnUser
        [DllImport("advapi32.dll")]

        public static extern bool LogonUser(string username, string password, int logType, int logpv, ref IntPtr intPtr);
        #endregion
        private readonly Serilog.ILogger _logger;
        public AuthenticationServices(Authentication authentication, Serilog.ILogger logger)
        {
            Authentication = authentication;
            _logger = logger;
        }

        public Authentication Authentication { get; set; }

        bool _isAuthenticated;

        public bool Authenticate()
        {
            try
            {
                IntPtr ip = IntPtr.Zero;
                _isAuthenticated = LogonUser(Authentication.Username, Authentication.Password, 2, 0, ref ip);

                return _isAuthenticated;
            }
            catch(Exception exception)
            {
                _logger.Error(exception.Message);
                throw;
            }
        }
    }
}
