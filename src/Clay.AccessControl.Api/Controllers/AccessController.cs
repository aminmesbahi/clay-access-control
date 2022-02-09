using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Clay.AccessControl.Api.Controllers;

[Produces("application/json")]
[ApiController]
[Route("v1/[controller]")]
public class AccessController : ControllerBase
{
    private readonly ILogger<AccessController> _logger;
    private readonly IAccessControlService _service;
    public AccessController(ILogger<AccessController> logger, IAccessControlService service)
    {
        _logger = logger;
        _service = service;
    }

    [SwaggerOperation("Get audit history with pagination")]
    [HttpGet("GetAuditList")]
    [ProducesResponseType(typeof(GetAuditListResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAuditListAsync(
            [FromQuery] UrlQueryParameters urlQueryParameters,
            CancellationToken cancellationToken)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var hobbies = await _service.GetAccessHistoryByPageAsync(
                                urlQueryParameters.Limit,
                                urlQueryParameters.Page,
                                cancellationToken);

        return Ok(hobbies);
    }

public record UrlQueryParameters(int Limit = 50, int Page = 1);

#pragma warning disable CS1573
    /// <summary>
    /// Authorize the tag for the lock
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Returns TRUE value if the tag is authorized for the lock; otherwise, it returns FALSE</returns>
    /// <remarks>
    /// Sample request:
    ///     
    ///     // Allowed tag sample:
    ///     {
    ///         "lock": "7025cdba-4810-47d9-acdc-99f48766c0aa",
    ///         "tag": "6f5f6b36-ace9-401e-8e97-5dea550e2b3d"
    ///     }
    ///
    /// </remarks>
    /// <response code="200">When User exists &amp; tag belongs to the user &amp; tag is active and the lock access granted to the tag</response>
    /// <response code="401">If tag was not authorized or activated (any reason)</response>
    [SwaggerOperation("Accepts tag and log IDs to check access and if authorized open the door")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(bool))]
    [HttpPost("AccessRequest")]
    public async Task<bool> GetWorkloadCalculationsHistoryAsync(AccessRequestDto request, CancellationToken cancellationToken)
    {
        IPAddress remoteIpAddress = HttpContext.Connection.RemoteIpAddress ?? IPAddress.Parse("127.0.0.1");
        return await _service.AccessRequestAsync(request, remoteIpAddress);
    }
#pragma warning restore CS1573
}