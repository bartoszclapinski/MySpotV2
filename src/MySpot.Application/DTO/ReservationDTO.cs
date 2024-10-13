using MySpot.Core.ValueObjects;

namespace MySpot.Application.DTO;

public class ReservationDTO
{
    public Guid Id { get; set; } 
    public Guid ParkingSpotId { get; set; }
    public string EmployeeName { get; set; }   
    public Date Date { get; set; }
}