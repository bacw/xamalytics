using Xamalytics.Application.Request.Commands;
using Xamalytics.Application.Request.Queries;
using Xamalytics.Common;
using Xamalytics.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Xamalytics.Api.Controllers
{
    /// <summary>
    /// Requests
    /// </summary>
    [Route("api/[controller]")]
    [EnableCors]
    [Authorize(Roles = $"{Constants.AdministratorGroupName},{Constants.SuperAdministratorGroupName}")]
    [ApiController]
    public class RequestController : BaseApiController
    {
        /// <summary>
        /// Get all requests
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("getAll")]
        public async Task<ActionResult<ServiceResult<List<RequestDto>>>> GetAllRequests(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetAllRequestsQuery(), cancellationToken));
        }

        /// <summary>
        /// Search requests
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("Search/{startDate}")]
        public async Task<ActionResult<ServiceResult<List<RequestDto>>>> SearchRequestsByDate(DateTime startDate, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new SearchRequestsQuery() { StartDate = startDate }, cancellationToken));
        }

        /// <summary>
        /// Get request by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResult<RequestDto>>> GetRequestById(int id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetRequestByIdQuery { RequestId = id }, cancellationToken));
        }

        /// <summary>
        /// Get request by BatchId
        /// </summary>
        /// <param name="batchId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("SearchByBatch/{batchId}")]
        public async Task<ActionResult<ServiceResult<RequestDto>>> GetRequestByBatchId(long batchId, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetRequestByBatchQuery { BatchId = batchId }, cancellationToken));
        }

        /// <summary>
        /// Create request
        /// </summary>
        /// <param name="realTimeInput"></param>
        /// <returns></returns>
        [HttpPost("{mrnNumber}")]
        [RequestSizeLimit(100 * 1024 * 1024)] //100MB
        public async Task<ActionResult<ServiceResult<RequestDto>>> Create([FromForm] RealTimeInput realTimeInput)
        {
            var command = new CreateRequestCommand() { Image = realTimeInput.file ?? throw new InvalidOperationException()};

            return Ok(await Mediator.Send(command, CancellationToken.None));
        }
    }
}
