using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Xamalytics.Services.Interface.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Xamalytics.Services.Implementation.Common
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IDateTimeService _dateTimeService;

        public TokenService(IConfiguration configuration, IDateTimeService dateTimeService)
        {
            _configuration = configuration;
            _dateTimeService = dateTimeService;
        }

        public string CreateJwtSecurityToken(string id)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: _dateTimeService.Now.AddDays(90),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
