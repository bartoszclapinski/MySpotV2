namespace MySpot.Core.Exceptions;

public class ReservationAlreadyExistsException(string name, DateTime date) 
    : CustomException($"Parking spot: {name} already has reservation for date: {date:d}.")
{
}
