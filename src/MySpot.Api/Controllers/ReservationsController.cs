using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Commands;
using MySpot.Api.Entities;
using MySpot.Api.Services;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("reservations")]
public class ReservationsController : ControllerBase
{
    private readonly ReservationService _reservationService = new();

    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> Get() => Ok(_reservationService.GetAllWeekly());    

    [HttpGet("{id}")]
    public ActionResult<Reservation> Get(Guid id)
    {
        var reservation = _reservationService.Get(id);
        if (reservation is null) return NotFound();
        return Ok(reservation);
    }

    [HttpPost]
    public ActionResult Post(CreateReservation command)
    {
        var id = _reservationService.Create(command with { ReservationId = Guid.NewGuid() });
        if (id is null) return BadRequest();

        return CreatedAtAction(nameof(Get), new { id  }, null);
    }

    [HttpPut("{id}")]
    public ActionResult Put(Guid id, ChangeReservationLicensePlate command)    {
        
        if (!_reservationService.Update(command with { ReservationId = id })) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        if (!_reservationService.Delete(new DeleteReservation(id))) return NotFound();
        return NoContent();
    }
}
