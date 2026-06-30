using Microsoft.AspNetCore.Mvc;

namespace Azor.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            status = "ok",
            app = "Azor.Api",
            message = "Backend do Azor está funcionando.",
            timestamp = DateTimeOffset.UtcNow
        });
    }
}