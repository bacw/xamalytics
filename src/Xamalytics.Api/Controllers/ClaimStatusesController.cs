using Xamalytics.Application.ClaimStatus.Queries;
using Xamalytics.Common;
using Xamalytics.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Xamalytics.Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors]
    [ApiController]
    [Authorize(Roles = $"{Constants.StandardGroupName},{Constants.AdministratorGroupName},{Constants.SuperAdministratorGroupName}")]
    public class ClaimStatusesController : BaseApiController
    {
        [HttpGet("getAll")]
        public async Task<ActionResult<ServiceResult<List<ClaimStatusesDto>>>> GetAllClaim(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetAllClaimsStatuses(), cancellationToken));
        }
    }
}
