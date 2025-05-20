using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExampleController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var data = new
        {
            Message = "Hello from the backend API!",
            Timestamp = DateTime.UtcNow
        };
        return Ok(data);
    }
}