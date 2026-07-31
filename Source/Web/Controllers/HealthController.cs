using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

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
            app = "Web",
            message = "Backend do Azor está funcionando.",
            timestamp = DateTimeOffset.UtcNow
        });
    }
}
