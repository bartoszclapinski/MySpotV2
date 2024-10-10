using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Commands;
using MySpot.Api.Entities;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("reservations")]
public class ReservationsController : ControllerBase
{
    private static readonly ReservationService ReservationService = new(
    [
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(DateTimeOffset.Now), "P1"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(DateTimeOffset.Now), "P2"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(DateTimeOffset.Now), "P3"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(DateTimeOffset.Now), "P4"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(DateTimeOffset.Now), "P5")
    ]);

    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> Get() => Ok(ReservationService.GetAllWeekly());    

    [HttpGet("{id}")]
    public ActionResult<Reservation> Get(Guid id)
    {
        ReservationDTO reservation = ReservationService.Get(id);
        if (reservation is null) return NotFound();
        return Ok(reservation);
    }

    [HttpPost]
    public ActionResult Post(CreateReservation command)
    {
        var id = ReservationService.Create(command with { ReservationId = Guid.NewGuid() });
        if (id is null) return BadRequest();

        return CreatedAtAction(nameof(Get), new { id  }, null);
    }

    [HttpPut("{id}")]
    public ActionResult Put(Guid id, ChangeReservationLicensePlate command)    {
        
        if (!ReservationService.Update(command with { ReservationId = id })) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        if (!ReservationService.Delete(new DeleteReservation(id))) return NotFound();
        return NoContent();
    }
}
