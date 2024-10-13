using MySpot.Core.ValueObjects;

namespace MySpot.Application.Commands
{
    public record CreateReservation(
        Guid ReservationId, Guid ParkingSpotId, string EmployeeName, string LicensePlate, Date Date);
}