public class ReservationDTO
{
    public Guid Id { get; set; } 
    public Guid ParkingSpotId { get; set; }
    public string EmployeeName { get; set; }   
    public DateTime Date { get; set; }
}
