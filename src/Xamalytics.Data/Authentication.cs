using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamalytics.Data
{

    public class Authentication
    {
        public string Username { get; set; } = string.Empty;    
        public string Password { get; set; } = string.Empty;
    }

    public static class AuthenticationValidate
    {
        public static string Validate(this Authentication authentication)
        {
           
            if (string.IsNullOrEmpty(authentication.Username))
            {
                return "UserName can't be null or empty";
            }
            else if (string.IsNullOrEmpty(authentication.Password))
            {
                return "Password can't be null or empty";
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
