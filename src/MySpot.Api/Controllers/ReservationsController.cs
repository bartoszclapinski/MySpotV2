using Microsoft.AspNetCore.Mvc;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("reservations")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}
