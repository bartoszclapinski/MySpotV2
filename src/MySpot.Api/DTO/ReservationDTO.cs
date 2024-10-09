using MySpot.Api.ValueObjects;

public class ReservationDTO
{
    public Guid Id { get; set; } 
    public Guid ParkingSpotId { get; set; }
    public string EmployeeName { get; set; }   
    public Date Date { get; set; }
}
