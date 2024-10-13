namespace MySpot.Core.Exceptions;

public class InvalidReservationDateException(DateTime date) 
    : CustomException($"Reservation cannot be made for date: {date:d}.")
{
    public DateTime Date { get; } = date;
}
