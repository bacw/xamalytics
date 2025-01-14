using Microsoft.AspNetCore.Identity;

namespace Xamalytics.Services.Implementation.Common.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
    }
}
