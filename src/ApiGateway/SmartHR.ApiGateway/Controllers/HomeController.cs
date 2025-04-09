using Microsoft.AspNetCore.Mvc;

namespace SmartHR.ApiGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    [HttpGet("status")]
    public IActionResult GetStatus()
    {
        return Ok(new { Status = "API Gateway is Running" });
    }
}