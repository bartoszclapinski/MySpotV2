using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Models;
using MySpot.Api.Services;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("reservations")]
public class ReservationsController : ControllerBase
{
    private readonly ReservationService _reservationService = new();

    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> Get() => Ok(_reservationService.GetAll());    

    [HttpGet("{id}")]
    public ActionResult<Reservation> Get(int id)
    {
        var reservation = _reservationService.Get(id);
        if (reservation is null) return NotFound();
        return Ok(reservation);
    }

    [HttpPost]
    public ActionResult Post(Reservation reservation)
    {
        var id = _reservationService.Create(reservation);
        if (id is null) return BadRequest();

        return CreatedAtAction(nameof(Get), new { id  }, null);
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, Reservation reservation)
    {
        reservation.Id = id;
        if (!_reservationService.Update(reservation)) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        if (!_reservationService.Delete(id)) return NotFound();
        return NoContent();
    }
}
