using MediatR;
using Xamalytics.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Xamalytics.Api.Controllers
{
    [ApiController]
    [EnableCors]
    [Authorize(Roles = $"{Constants.StandardGroupName},{Constants.AdministratorGroupName},{Constants.SuperAdministratorGroupName}")]
    [Route("api/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        private ISender _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
    }
}