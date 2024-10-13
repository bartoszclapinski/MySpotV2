using Microsoft.AspNetCore.Mvc;
using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Application.Services;
using MySpot.Core.Entities;


namespace MySpot.Api.Controllers;

[ApiController]
[Route("reservations")]
public class ReservationsController(ReservationService reservationService) : ControllerBase
{

    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> Get() => Ok(reservationService.GetAllWeekly());    

    [HttpGet("{id}")]
    public ActionResult<Reservation> Get(Guid id)
    {
        ReservationDTO reservation = reservationService.Get(id);
        if (reservation is null) return NotFound();
        return Ok(reservation);
    }

    [HttpPost]
    public ActionResult Post(CreateReservation command)
    {
        var id = reservationService.Create(command with { ReservationId = Guid.NewGuid() });
        if (id is null) return BadRequest();

        return CreatedAtAction(nameof(Get), new { id  }, null);
    }

    [HttpPut("{id}")]
    public ActionResult Put(Guid id, ChangeReservationLicensePlate command)    {
        
        if (!reservationService.Update(command with { ReservationId = id })) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        if (!reservationService.Delete(new DeleteReservation(id))) return NotFound();
        return NoContent();
    }
}
