using System;
using MySpot.Api.Exceptions;
using MySpot.Api.Models;

namespace MySpot.Api.Entities;

public class WeeklyParkingSpot
{
    private readonly HashSet<Reservation> _reservations = [];

    public Guid Id { get; }
    public DateTime From { get; }
    public DateTime To { get; }
    public string Name { get; }
    public IEnumerable<Reservation> Reservations => _reservations;

    public WeeklyParkingSpot(Guid id, DateTime from, DateTime to, string name)
    {
        Id = id;
        From = from;
        To = to;
        Name = name;
    }

    public void AddReservation(Reservation reservation)
    {       
        var isInvalidDate = reservation.Date.Date < From 
            || reservation.Date.Date > To 
            || reservation.Date.Date < DateTime.UtcNow.Date;
            
        if (isInvalidDate) 
            throw new InvalidReservationDateException(reservation.Date);

        var reservationAlreadyExists = _reservations
            .Any(x => x.Date == reservation.Date);
        
        if (reservationAlreadyExists)
            throw new ReservationAlreadyExistsException(Name, reservation.Date);

        _reservations.Add(reservation);
    }

    public bool RemoveReservation(Guid reservationId) 
        => _reservations.RemoveWhere(x => x.Id == reservationId) > 0;
}
