using Microsoft.AspNetCore.Mvc;

namespace Clay.AccessControl.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AccessController : ControllerBase
{
    private readonly ILogger<AccessController> _logger;

    public AccessController(ILogger<AccessController> logger)
    {
        _logger = logger;
    }
}