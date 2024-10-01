using MySpot.Api.Models;

namespace MySpot.Api.Services;

public class ReservationService
{
    private static readonly List<string> ParkingSpotNames = new() {"P1", "P2", "P3", "P4", "P5"};
    private static int _id = 1;
    private static readonly List<Reservation> Reservations = new();

    public Reservation Get(int id) => Reservations.SingleOrDefault(x => x.Id == id);

    public IEnumerable<Reservation> GetAll() => Reservations;

    public int? Create(Reservation reservation)
    {
        var now = DateTime.UtcNow.Date;
        var pastDays = now.DayOfWeek is DayOfWeek.Sunday ? 7 : (int)now.DayOfWeek;
        var remainingDays = 7 - pastDays;

        if (ParkingSpotNames.All(x => x != reservation.ParkingSpotName)) return default;

        if (!(reservation.Date.Date >= now.Date && reservation.Date.Date < now.Date.AddDays(remainingDays))) 
            return default;

        var reservationAlreadyExists = Reservations
            .Any(x => x.ParkingSpotName == reservation.ParkingSpotName && x.Date == reservation.Date);
        if (reservationAlreadyExists) return default;
        reservation.Id = _id++;
        Reservations.Add(reservation);
        return reservation.Id;
    }

    public bool Update(Reservation reservation)
    {
        var existingReservation = Get(reservation.Id);

        if (existingReservation is null) return false;
        if (existingReservation.Date < DateTime.UtcNow.Date) return false;
        
        existingReservation.LicensePlate = reservation.LicensePlate;
        return true;
    }

    public bool Delete(int id)
    {
        var reservationToDelete = Get(id);
        if (reservationToDelete is null) return false;
        return Reservations.Remove(reservationToDelete);
    }
}