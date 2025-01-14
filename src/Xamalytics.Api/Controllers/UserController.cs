using Xamalytics.Dto;
using Microsoft.AspNetCore.Mvc;
using Xamalytics.Application.User.Queries;
using Xamalytics.Application.User.Commands;
using Xamalytics.Common;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Xamalytics.Services.Interface.Common;
using Xamalytics.Services.Interface;
using System.Security.Principal;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace Xamalytics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    [Authorize(Roles = $"{Constants.StandardGroupName},{Constants.AdministratorGroupName},{Constants.SuperAdministratorGroupName}")]
    public class UserController : BaseApiController
    {
        private readonly IAuditUserService _auditUserService;
        private readonly IDateTimeService _dateService;
        private readonly IUserService _userService;
        private readonly AppSetting _appSetting;

        public UserController(IAuditUserService auditUserService, IUserService userService, IDateTimeService dateService, IOptions<AppSetting> options)
        {
            _auditUserService = auditUserService;
            _dateService = dateService;
            _userService = userService;
            _appSetting = options.Value;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<ServiceResult<List<UserDto>>>> GetAllUser(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetAllUserQuery(), cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResult<UserDto>>> GetUserById(int id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetUserByIdQuery { UserId = id }, cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResult<UserDto>>> LogIn(CancellationToken cancellationToken)
        {
            if (HttpContext != null && HttpContext.User != null && HttpContext.User.Identity != null)
            {
                var identity = HttpContext.User.Identity as WindowsIdentity;
                if (identity == null || 
                    string.IsNullOrEmpty(identity.Name) || 
                    identity.Groups == null || 
                    identity.Groups.Count == 0) 
                    
                    return Unauthorized();
                                
                var isAuthenticated = false;
                var userClaimStatusId = GetClaimStatusId(identity.Groups, identity);
                
                if (userClaimStatusId == 0) return Unauthorized();                

                if(userClaimStatusId  != 0) isAuthenticated = true;

                var user = await _userService.GetUserByName(identity.Name, cancellationToken);
                if (user == null)
                {
                    var command = new CreateUserCommand()
                    {
                        Username = identity.Name,
                        ClaimStatusId = userClaimStatusId,
                        CreatedDate = _dateService.Now,
                    };

                    user = (await Mediator.Send(command, cancellationToken)).Data;
                }
                else 
                {
                    var command = new UpdateUserCommand()
                    {
                        Username = identity.Name,
                        ClaimStatusId = userClaimStatusId,
                    };

                    user = (await Mediator.Send(command, cancellationToken)).Data;
                }

                if (user != null && isAuthenticated)
                {
                    var key = new SymmetricSecurityKey(Encoding.UTF32.GetBytes("efucvujhUEFWF768924R23764R42809JHIFGVW74f329jklsfvbst6348t"));
                    var signInCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var dateToday = _dateService.Now;

                    //generate JWT token
                    var token = new JwtSecurityToken(
                        issuer: "Xamalytics",
                        audience: "Xamalytics",
                        notBefore: dateToday,
                        expires: _dateService.Now.AddMinutes(30),
                        signingCredentials: signInCredentials
                     );

                    var tokenGenerated = new JwtSecurityTokenHandler().WriteToken(token);

                    var response = new UserDto
                    {
                        Username = identity.Name,
                        CreatedDate = user.CreatedDate,
                        IsAuthenticated = isAuthenticated,
                        ClaimStatusId = user.ClaimStatusId,
                        Token = tokenGenerated,
                        Id = user.Id
                    };

                    var auditUserObject = new AuditUserDto
                    {
                        ActivityTypeId = (int)Enums.ActivityType.LogIn,
                        UserName = user.Username,
                        ActivityDetails = $"{user.Username}"
                    };

                    await _auditUserService.AddAuditUser(auditUserObject, tokenGenerated, cancellationToken);

                    return Ok(ServiceResult.Success(response));
                }

                return Unauthorized();
            }
            else
            {
                return Unauthorized();
            }
        }

        private int GetClaimStatusId(IdentityReferenceCollection groups, WindowsIdentity identity)
        {
            var userClaimStatusId = 0;

            WindowsPrincipal windowsPrincipal = new WindowsPrincipal(identity);

            if (windowsPrincipal.IsInRole(_appSetting.SuperAdministratorGroupName)) userClaimStatusId = (int)Enums.ClaimsStatuses.SuperAdminstrator;
            if (windowsPrincipal.IsInRole(_appSetting.AdministratorGroupName)) userClaimStatusId = (int)Enums.ClaimsStatuses.Administrator;
            if (windowsPrincipal.IsInRole(_appSetting.StandardGroupName)) userClaimStatusId = (int)Enums.ClaimsStatuses.Standard;

            return userClaimStatusId;
        }
    }
}
