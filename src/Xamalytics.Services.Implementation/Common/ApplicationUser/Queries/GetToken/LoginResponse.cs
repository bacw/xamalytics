using MediViewerLite.Dto.Common;

namespace MediViewerLite.Services.Implementation.Common.ApplicationUser.Queries.GetToken
{
    public class LoginResponse
    {
        public ApplicationUserDto User { get; set; }

        public string Token { get; set; }
    }
}
