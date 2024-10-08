
using MySpot.Api.Exceptions;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Entities;

public class WeeklyParkingSpot
{
    private readonly HashSet<Reservation> _reservations = [];

    public Guid Id { get; }
    public Week Week { get; }
    public string Name { get; }
    public IEnumerable<Reservation> Reservations => _reservations;

    public WeeklyParkingSpot(Guid id, Week week, string name)
    {
        Id = id;
        Week = week;
        Name = name;
    }

    public void AddReservation(Reservation reservation, Date now)
    {       
        var isInvalidDate = reservation.Date < Week.From 
            || reservation.Date > Week.To 
            || reservation.Date < now;
            
        if (isInvalidDate) 
            throw new InvalidReservationDateException(reservation.Date.Value.Date);

        var reservationAlreadyExists = _reservations
            .Any(x => x.Date == reservation.Date);
        
        if (reservationAlreadyExists)
            throw new ReservationAlreadyExistsException(Name, reservation.Date.Value.Date);

        _reservations.Add(reservation);
    }

    public bool RemoveReservation(Guid reservationId) 
        => _reservations.RemoveWhere(x => x.Id == reservationId) > 0;
}
